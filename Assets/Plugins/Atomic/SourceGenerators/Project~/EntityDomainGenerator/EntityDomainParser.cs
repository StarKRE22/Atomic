using System;
using System.Collections.Generic;
using System.Linq;
using EntityDomainGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityDomainGenerator
{
    /// <summary>
    /// Parses <c>[EntityDomainBehaviours(typeof(...))]</c>-annotated classes
    /// from Roslyn syntax + semantic models.
    /// </summary>
    internal static class EntityDomainParser
    {
        private const string AttributeName = "EntityDomainBehaviours";
        private const string EntityInterfaceName = "IEntity";

        private static readonly IReadOnlyList<BehaviourInterface> BehaviourVariants = new[]
        {
            new BehaviourInterface("Behaviour", "IEntityBehaviour", "IEntityBehaviour"),
            new BehaviourInterface("Tick", "IEntityTick", "IEntityTick"),
            new BehaviourInterface("FixedTick", "IEntityFixedTick", "IEntityFixedTick"),
            new BehaviourInterface("LateTick", "IEntityLateTick", "IEntityLateTick"),
            new BehaviourInterface("Init", "IEntityInit", "IEntityInit"),
            new BehaviourInterface("Enable", "IEntityEnable", "IEntityEnable"),
            new BehaviourInterface("Disable", "IEntityDisable", "IEntityDisable"),
            new BehaviourInterface("Dispose", "IEntityDispose", "IEntityDispose"),
            new BehaviourInterface("Gizmos", "IEntityGizmos", "IEntityGizmos")
        };

        /// <summary>
        /// Quick syntax check — does this node look like a class with
        /// <c>[EntityDomainBehaviours]</c>?
        /// </summary>
        public static bool IsCandidate(SyntaxNode node)
        {
            if (node is not ClassDeclarationSyntax classDecl)
                return false;

            return classDecl.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(attr => IsAttributeName(attr));
        }

        /// <summary>
        /// Semantic transform — extracts <see cref="EntityDomainDefinition"/> from a candidate class.
        /// Returns <c>null</c> if the class doesn't actually have the attribute at the semantic level
        /// or if the entity type argument is invalid.
        /// </summary>
        public static EntityDomainDefinition? Transform(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDecl)
                return null;

            SemanticModel semanticModel = context.SemanticModel;

            INamedTypeSymbol? classSymbol = semanticModel.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;
            if (classSymbol == null)
                return null;

            AttributeData? attribute = FindAttribute(classSymbol.GetAttributes());
            if (attribute == null)
                return null;

            ITypeSymbol? entityType = ExtractEntityType(attribute);
            if (entityType == null)
                return null;

            if (!ImplementsIEntity(entityType))
                return null;

            string ns = GetNamespace(classDecl);
            string entityTypeName = entityType.ToDisplayString();
            string entityShortName = entityType.Name;

            return new EntityDomainDefinition(
                ns: ns,
                className: classDecl.Identifier.Text,
                entityTypeName: entityTypeName,
                entityShortName: entityShortName,
                behaviours: BehaviourVariants
            );
        }

        private static bool IsAttributeName(AttributeSyntax attr)
        {
            string? name = attr.Name switch
            {
                IdentifierNameSyntax id => id.Identifier.Text,
                QualifiedNameSyntax q => q.Right.Identifier.Text,
                _ => null
            };

            return name == AttributeName || name == AttributeName + "Attribute";
        }

        private static AttributeData? FindAttribute(IEnumerable<AttributeData> attributes)
        {
            foreach (var attr in attributes)
            {
                if (attr.AttributeClass?.Name == AttributeName ||
                    attr.AttributeClass?.Name == AttributeName + "Attribute")
                {
                    return attr;
                }
            }

            return null;
        }

        private static ITypeSymbol? ExtractEntityType(AttributeData attribute)
        {
            if (attribute.ConstructorArguments.Length == 0)
                return null;

            TypedConstant arg = attribute.ConstructorArguments[0];
            if (arg.Kind != TypedConstantKind.Type)
                return null;

            return arg.Value as ITypeSymbol;
        }

        private static bool ImplementsIEntity(ITypeSymbol type)
        {
            if (type.TypeKind != TypeKind.Interface &&
                type.TypeKind != TypeKind.Class &&
                type.TypeKind != TypeKind.Struct)
            {
                return false;
            }

            if (type.Name == EntityInterfaceName &&
                type.ContainingNamespace?.ToDisplayString() == "Atomic.Entities")
            {
                return true;
            }

            foreach (var iface in type.AllInterfaces)
            {
                if (iface.Name == EntityInterfaceName &&
                    iface.ContainingNamespace?.ToDisplayString() == "Atomic.Entities")
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetNamespace(SyntaxNode node)
        {
            for (var current = node.Parent; current != null; current = current.Parent)
            {
                if (current is BaseNamespaceDeclarationSyntax ns)
                    return ns.Name.ToString();
            }

            return string.Empty;
        }
    }
}

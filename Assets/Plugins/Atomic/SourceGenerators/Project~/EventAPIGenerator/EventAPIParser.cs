using System.Collections.Generic;
using System.Linq;
using EventAPIGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EventAPIGenerator
{
    /// <summary>
    /// Parses <c>[EventExtensionsAPI]</c>-annotated classes from Roslyn syntax + semantic models.
    /// </summary>
    internal static class EventAPIParser
    {
        private const string EventAPIAttributeName = "EventExtensionsAPI";
        private const string EventKeyTypeName = "EventKey";
        private const string UnsafeAttributeName = "Unsafe";
        private const string AtomicEventsNamespace = "Atomic.Events";

        /// <summary>
        /// Quick syntax check — does this node look like a class with <c>[EventExtensionsAPI]</c>?
        /// Runs before the semantic model is available (cheap).
        /// </summary>
        public static bool IsCandidate(SyntaxNode node)
        {
            if (node is not ClassDeclarationSyntax classDecl)
                return false;

            return classDecl.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(attr => IsEventAPIAttributeName(attr));
        }

        /// <summary>
        /// Semantic transform — extracts <see cref="EventAPIDefinition"/> from a candidate class.
        /// Returns <c>null</c> if the class doesn't actually have the attribute at the semantic level.
        /// </summary>
        public static EventAPIDefinition? Transform(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDecl)
                return null;

            SemanticModel semanticModel = context.SemanticModel;

            INamedTypeSymbol? classSymbol = semanticModel.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;
            if (classSymbol == null)
                return null;

            if (!TryGetEventAPIAttribute(classSymbol.GetAttributes(), out AttributeData? attributeData))
                return null;

            bool classUnsafe = TryGetNamedArgBool(attributeData!, "Unsafe");

            string ns = GetNamespace(classDecl);
            var events = new List<EventField>();

            foreach (var member in classDecl.Members)
            {
                if (member is not FieldDeclarationSyntax fieldDecl)
                    continue;

                if (!fieldDecl.Modifiers.Any(SyntaxKind.StaticKeyword))
                    continue;

                if (fieldDecl.Declaration.Variables.Count != 1)
                    continue;

                var variable = fieldDecl.Declaration.Variables[0];
                string fieldName = variable.Identifier.Text;

                var fieldSymbol = semanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                if (fieldSymbol == null)
                    continue;

                if (fieldSymbol.Type is not INamedTypeSymbol namedType ||
                    namedType.ContainingNamespace?.ToDisplayString() != AtomicEventsNamespace ||
                    namedType.Name != EventKeyTypeName)
                {
                    continue;
                }

                int arity = namedType.TypeArguments.Length;
                if (arity < 1 || arity > 4)
                    continue;

                string busTypeName = namedType.TypeArguments[0].ToDisplayString();
                var argTypeNames = new List<string>(arity - 1);
                for (int i = 1; i < arity; i++)
                    argTypeNames.Add(namedType.TypeArguments[i].ToDisplayString());

                bool fieldUnsafe = classUnsafe || HasUnsafeAttribute(fieldDecl);

                events.Add(new EventField(fieldName, busTypeName, argTypeNames.AsReadOnly(), fieldUnsafe));
            }

            return new EventAPIDefinition(
                ns: ns,
                className: classDecl.Identifier.Text,
                unsafeFlag: classUnsafe,
                events: events.AsReadOnly()
            );
        }

        private static bool IsEventAPIAttributeName(AttributeSyntax attr)
        {
            string? name = attr.Name switch
            {
                IdentifierNameSyntax id => id.Identifier.Text,
                QualifiedNameSyntax q => q.Right.Identifier.Text,
                _ => null
            };

            return name == EventAPIAttributeName ||
                   name == EventAPIAttributeName + "Attribute";
        }

        private static bool TryGetEventAPIAttribute(IEnumerable<AttributeData> attributes, out AttributeData? result)
        {
            foreach (var attr in attributes)
            {
                if (attr.AttributeClass?.Name == EventAPIAttributeName ||
                    attr.AttributeClass?.Name == EventAPIAttributeName + "Attribute")
                {
                    result = attr;
                    return true;
                }
            }

            result = null;
            return false;
        }

        private static bool TryGetNamedArgBool(AttributeData attribute, string argName)
        {
            foreach (var kvp in attribute.NamedArguments)
            {
                if (kvp.Key == argName &&
                    kvp.Value.Kind == TypedConstantKind.Primitive &&
                    kvp.Value.Value is bool value)
                {
                    return value;
                }
            }

            return false;
        }

        private static bool HasUnsafeAttribute(FieldDeclarationSyntax fieldDecl)
        {
            return fieldDecl.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(attr =>
                {
                    string? name = attr.Name switch
                    {
                        IdentifierNameSyntax id => id.Identifier.Text,
                        QualifiedNameSyntax q => q.Right.Identifier.Text,
                        _ => null
                    };
                    return name == UnsafeAttributeName ||
                           name == UnsafeAttributeName + "Attribute";
                });
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

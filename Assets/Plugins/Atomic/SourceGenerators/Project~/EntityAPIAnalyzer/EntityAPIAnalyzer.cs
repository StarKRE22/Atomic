using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace EntityAPIAnalyzer
{
    /// <summary>
    /// Reports diagnostics for <c>[EntityExtensionsAPI]</c> class declarations.
    /// Currently validates that <c>ValueKey&lt;&gt;</c> and <c>TagKey&lt;&gt;</c> fields
    /// are properly initialized.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class EntityAPIAnalyzer : DiagnosticAnalyzer
    {
        private const string EntityAPIAttributeName = "EntityExtensionsAPI";
        private const string ValueKeyTypeName = "ValueKey";
        private const string TagKeyTypeName = "TagKey";
        private const string AtomicEntitiesNamespace = "Atomic.Entities";

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(
                EntityAPIDiagnostics.MissingInitializer,
                EntityAPIDiagnostics.ParameterlessConstructor);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeField, SyntaxKind.FieldDeclaration);
        }

        private static void AnalyzeField(SyntaxNodeAnalysisContext context)
        {
            var fieldDecl = (FieldDeclarationSyntax)context.Node;

            // Only static fields
            if (!fieldDecl.Modifiers.Any(SyntaxKind.StaticKeyword))
                return;

            // Only fields inside [EntityExtensionsAPI] classes
            if (fieldDecl.Parent is not ClassDeclarationSyntax classDecl)
                return;

            if (!HasEntityAPIAttribute(classDecl))
                return;

            foreach (var variable in fieldDecl.Declaration.Variables)
            {
                // Only ValueKey<> / TagKey<> from Atomic.Entities
                if (!IsKeyField(context.SemanticModel, variable, context.CancellationToken))
                    continue;

                string fieldName = variable.Identifier.Text;
                var initializer = variable.Initializer;

                // No initializer at all
                if (initializer == null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        EntityAPIDiagnostics.MissingInitializer,
                        variable.Identifier.GetLocation(),
                        fieldName));
                    continue;
                }

                // Check for parameterless construction or default
                if (IsParameterlessOrDefault(initializer.Value))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        EntityAPIDiagnostics.ParameterlessConstructor,
                        initializer.Value.GetLocation(),
                        fieldName));
                }
            }
        }

        private static bool HasEntityAPIAttribute(ClassDeclarationSyntax classDecl)
        {
            foreach (var attrList in classDecl.AttributeLists)
            {
                foreach (var attr in attrList.Attributes)
                {
                    string? name = attr.Name switch
                    {
                        IdentifierNameSyntax id => id.Identifier.Text,
                        QualifiedNameSyntax q => q.Right.Identifier.Text,
                        _ => null
                    };

                    if (name == EntityAPIAttributeName || name == EntityAPIAttributeName + "Attribute")
                        return true;
                }
            }

            return false;
        }

        private static bool IsKeyField(SemanticModel semanticModel, VariableDeclaratorSyntax variable, CancellationToken cancellationToken)
        {
            var symbol = semanticModel.GetDeclaredSymbol(variable, cancellationToken) as IFieldSymbol;
            if (symbol?.Type is not INamedTypeSymbol namedType)
                return false;

            return namedType.ContainingNamespace?.ToDisplayString() == AtomicEntitiesNamespace &&
                   (namedType.Name == ValueKeyTypeName || namedType.Name == TagKeyTypeName);
        }

        private static bool IsParameterlessOrDefault(ExpressionSyntax expression)
        {
            return expression switch
            {
                // default or default(ValueKey<...>)
                DefaultExpressionSyntax => true,
                LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.DefaultLiteralExpression) => true,

                // new() or new ValueKey<...>()
                ObjectCreationExpressionSyntax creation
                    => creation.ArgumentList == null || creation.ArgumentList.Arguments.Count == 0,
                ImplicitObjectCreationExpressionSyntax implicitCreation
                    => implicitCreation.ArgumentList == null || implicitCreation.ArgumentList.Arguments.Count == 0,

                _ => false
            };
        }
    }
}

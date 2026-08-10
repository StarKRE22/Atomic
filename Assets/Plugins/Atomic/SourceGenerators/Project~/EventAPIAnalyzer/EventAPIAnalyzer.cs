using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace EventAPIAnalyzer
{
    /// <summary>
    /// Reports diagnostics for <c>[EventExtensionsAPI]</c> class declarations.
    /// Currently validates that <c>EventKey&lt;&gt;</c> fields are properly initialized.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class EventAPIAnalyzer : DiagnosticAnalyzer
    {
        private const string EventAPIAttributeName = "EventExtensionsAPI";
        private const string EventKeyTypeName = "EventKey";
        private const string AtomicEventsNamespace = "Atomic.Events";

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(
                EventAPIDiagnostics.MissingInitializer,
                EventAPIDiagnostics.ParameterlessConstructor);

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

            // Only fields inside [EventExtensionsAPI] classes
            if (fieldDecl.Parent is not ClassDeclarationSyntax classDecl)
                return;

            if (!HasEventAPIAttribute(classDecl))
                return;

            foreach (var variable in fieldDecl.Declaration.Variables)
            {
                // Only EventKey<> from Atomic.Events
                if (!IsEventKeyField(context.SemanticModel, variable, context.CancellationToken))
                    continue;

                string fieldName = variable.Identifier.Text;
                var initializer = variable.Initializer;

                // No initializer at all
                if (initializer == null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        EventAPIDiagnostics.MissingInitializer,
                        variable.Identifier.GetLocation(),
                        fieldName));
                    continue;
                }

                // Check for parameterless construction or default
                if (IsParameterlessOrDefault(initializer.Value))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        EventAPIDiagnostics.ParameterlessConstructor,
                        initializer.Value.GetLocation(),
                        fieldName));
                }
            }
        }

        private static bool HasEventAPIAttribute(ClassDeclarationSyntax classDecl)
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

                    if (name == EventAPIAttributeName || name == EventAPIAttributeName + "Attribute")
                        return true;
                }
            }

            return false;
        }

        private static bool IsEventKeyField(SemanticModel semanticModel, VariableDeclaratorSyntax variable, CancellationToken cancellationToken)
        {
            var symbol = semanticModel.GetDeclaredSymbol(variable, cancellationToken) as IFieldSymbol;
            if (symbol?.Type is not INamedTypeSymbol namedType)
                return false;

            return namedType.ContainingNamespace?.ToDisplayString() == AtomicEventsNamespace &&
                   namedType.Name == EventKeyTypeName &&
                   namedType.TypeArguments.Length >= 1 &&
                   namedType.TypeArguments.Length <= 4;
        }

        private static bool IsParameterlessOrDefault(ExpressionSyntax expression)
        {
            return expression switch
            {
                DefaultExpressionSyntax => true,
                LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.DefaultLiteralExpression) => true,
                ObjectCreationExpressionSyntax creation
                    => creation.ArgumentList == null || creation.ArgumentList.Arguments.Count == 0,
                ImplicitObjectCreationExpressionSyntax implicitCreation
                    => implicitCreation.ArgumentList == null || implicitCreation.ArgumentList.Arguments.Count == 0,
                _ => false
            };
        }
    }
}

using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityAPIAnalyzer
{
    /// <summary>
    /// Provides code fixes for <see cref="EntityAPIDiagnostics.MissingInitializer"/> and
    /// <see cref="EntityAPIDiagnostics.ParameterlessConstructor"/>.
    /// Inserts or replaces the initializer with <c>new(nameof(FieldName))</c>.
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EntityAPICodeFixProvider)), Shared]
    public sealed class EntityAPICodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
            => ImmutableArray.Create(
                EntityAPIDiagnostics.MissingInitializerId,
                EntityAPIDiagnostics.ParameterlessConstructorId);

        public sealed override FixAllProvider GetFixAllProvider()
            => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            if (root == null)
                return;

            foreach (var diagnostic in context.Diagnostics)
            {
                var node = root.FindNode(diagnostic.Location.SourceSpan, getInnermostNodeForTie: true);
                var variable = FindVariableDeclarator(node);
                if (variable == null)
                    continue;

                var title = $"Initialize '{variable.Identifier.Text}' with nameof({variable.Identifier.Text})";
                context.RegisterCodeFix(
                    CodeAction.Create(
                        title: title,
                        createChangedDocument: c => AddNameOfInitializer(context.Document, variable, c),
                        equivalenceKey: nameof(EntityAPICodeFixProvider)),
                    diagnostic);
            }
        }

        private static VariableDeclaratorSyntax? FindVariableDeclarator(SyntaxNode node)
        {
            return node as VariableDeclaratorSyntax ?? node.FirstAncestorOrSelf<VariableDeclaratorSyntax>();
        }

        private static async Task<Document> AddNameOfInitializer(
            Document document,
            VariableDeclaratorSyntax variable,
            CancellationToken cancellationToken)
        {
            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            if (oldRoot == null)
                return document;

            var fieldName = variable.Identifier.Text;
            var nameOfExpression = SyntaxFactory.InvocationExpression(
                    SyntaxFactory.IdentifierName("nameof"),
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Argument(
                                SyntaxFactory.IdentifierName(fieldName)))))
                .WithTrailingTrivia(SyntaxFactory.Space);

            var newObjectCreation = SyntaxFactory.ImplicitObjectCreationExpression(
                    SyntaxFactory.Token(SyntaxKind.NewKeyword).WithTrailingTrivia(SyntaxFactory.Space),
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Argument(nameOfExpression))),
                    default);

            var newInitializer = SyntaxFactory.EqualsValueClause(
                    SyntaxFactory.Token(SyntaxKind.EqualsToken).WithTrailingTrivia(SyntaxFactory.Space),
                    newObjectCreation)
                .WithLeadingTrivia(SyntaxFactory.Space);

            var newVariable = variable.WithInitializer(newInitializer);
            var newRoot = oldRoot.ReplaceNode(variable, newVariable);

            return document.WithSyntaxRoot(newRoot);
        }
    }
}

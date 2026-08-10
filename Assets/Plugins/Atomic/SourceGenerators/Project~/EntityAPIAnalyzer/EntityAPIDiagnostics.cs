using Microsoft.CodeAnalysis;

namespace EntityAPIAnalyzer
{
    /// <summary>
    /// Diagnostic descriptors reported by the Entity API analyzer.
    /// </summary>
    internal static class EntityAPIDiagnostics
    {
        public const string MissingInitializerId = "EAPI0001";

        public static readonly DiagnosticDescriptor MissingInitializer = new DiagnosticDescriptor(
            id: MissingInitializerId,
            title: "Entity API key field must be initialized",
            messageFormat: "Entity API key field '{0}' must be initialized with a non-default value (e.g. `new(nameof({0}))`).",
            category: "Atomic.Entities",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "ValueKey/TagKey fields in [EntityExtensionsAPI] classes must be initialized so their Id is computed before use.");

        public const string ParameterlessConstructorId = "EAPI0002";

        public static readonly DiagnosticDescriptor ParameterlessConstructor = new DiagnosticDescriptor(
            id: ParameterlessConstructorId,
            title: "Entity API key field cannot use parameterless construction",
            messageFormat: "Entity API key field '{0}' cannot be initialized with `new()` or `default`; provide a name or id (e.g. `new(nameof({0}))`).",
            category: "Atomic.Entities",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "Parameterless construction leaves the key Id at its default value, which is invalid for entity API generation.");
    }
}

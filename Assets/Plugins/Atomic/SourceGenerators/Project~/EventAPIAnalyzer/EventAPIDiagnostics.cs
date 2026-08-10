using Microsoft.CodeAnalysis;

namespace EventAPIAnalyzer
{
    /// <summary>
    /// Diagnostic descriptors reported by the Event API analyzer.
    /// </summary>
    internal static class EventAPIDiagnostics
    {
        public const string MissingInitializerId = "EAPI0001";

        public static readonly DiagnosticDescriptor MissingInitializer = new DiagnosticDescriptor(
            id: MissingInitializerId,
            title: "Event API key field must be initialized",
            messageFormat: "Event API key field '{0}' must be initialized with a non-default value (e.g. `new(nameof({0}))`).",
            category: "Atomic.Events",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "EventKey fields in [EventExtensionsAPI] classes must be initialized so their Id is computed before use.");

        public const string ParameterlessConstructorId = "EAPI0002";

        public static readonly DiagnosticDescriptor ParameterlessConstructor = new DiagnosticDescriptor(
            id: ParameterlessConstructorId,
            title: "Event API key field cannot use parameterless construction",
            messageFormat: "Event API key field '{0}' cannot be initialized with `new()` or `default`; provide a name or id (e.g. `new(nameof({0}))`).",
            category: "Atomic.Events",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "Parameterless construction leaves the key Id at its default value, which is invalid for event API generation.");
    }
}

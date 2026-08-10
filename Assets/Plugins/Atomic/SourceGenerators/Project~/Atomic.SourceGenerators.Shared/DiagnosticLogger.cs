using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Atomic.SourceGenerators.Shared
{
    /// <summary>
    /// Accumulates diagnostics for a single generated class definition
    /// and reports them all on <see cref="Dispose"/>.
    /// Enriches each diagnostic with the class name as context.
    /// Mirrors Unity.Entities.SourceGen.Common.DiagnosticLogger approach.
    /// </summary>
    internal sealed class DiagnosticLogger : IDisposable
    {
        readonly SourceProductionContext _context;
        readonly string _className;
        readonly string _category;
        readonly List<Diagnostic> _pending = new List<Diagnostic>();

        public DiagnosticLogger(SourceProductionContext context, string className, string category)
        {
            _context = context;
            _className = className;
            _category = category;
        }

        /// <summary>Log an error. Will be reported on Dispose().</summary>
        public void LogError(string code, string title, string message, Location? location = null)
            => Add(DiagnosticSeverity.Error, code, title, message, location);

        /// <summary>Log a warning. Will be reported on Dispose().</summary>
        public void LogWarning(string code, string title, string message, Location? location = null)
            => Add(DiagnosticSeverity.Warning, code, title, message, location);

        /// <summary>Log an info message. Will be reported on Dispose().</summary>
        public void LogInfo(string code, string title, string message, Location? location = null)
            => Add(DiagnosticSeverity.Info, code, title, message, location);

        void Add(DiagnosticSeverity severity, string code, string title, string message, Location? location)
        {
            // Enrich message with class context (like Unity's IDiagnosticFrame)
            string enriched = $"[{_className}] {message}";

            var descriptor = new DiagnosticDescriptor(
                code, title, enriched, _category, severity, isEnabledByDefault: true);

            _pending.Add(Diagnostic.Create(descriptor, location ?? Location.None));
        }

        /// <summary>
        /// Reports all accumulated diagnostics to the <see cref="SourceProductionContext"/>.
        /// </summary>
        public void Dispose()
        {
            foreach (var diagnostic in _pending)
                _context.ReportDiagnostic(diagnostic);

            _pending.Clear();
        }
    }
}

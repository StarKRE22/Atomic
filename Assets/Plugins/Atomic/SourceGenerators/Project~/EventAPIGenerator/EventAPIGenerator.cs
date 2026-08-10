using System;
using System.Linq;
using System.Reflection;
using Atomic.SourceGenerators.Shared;
using EventAPIGenerator.Models;
using Microsoft.CodeAnalysis;

namespace EventAPIGenerator
{
    /// <summary>
    /// Incremental source generator that reads <c>[EventExtensionsAPI]</c>-marked classes
    /// and generates extension methods for Atomic event keys.
    /// </summary>
    [Generator]
    public sealed class EventAPIGenerator : IIncrementalGenerator
    {
        public const string Id = "EventAPIGenerator";

        /// <summary>
        /// Name of the assembly that defines the <c>[EventExtensionsAPI]</c> attribute.
        /// </summary>
        internal static readonly string CodegenAssemblyName = "Atomic.Events";

        /// <summary>
        /// <c>true</c> when running as part of a compiler invocation (not IDE analysis).
        /// In the IDE, source generators can run multiple times per keystroke;
        /// skipping there improves responsiveness. Generated types will still
        /// be available on the next actual build/domain reload.
        /// </summary>
        internal static readonly bool IsBuildTime = Assembly.GetEntryAssembly() != null;

        /// <summary>
        /// Determines whether this generator should run for the given compilation.
        /// Skips IDE analysis, non-referencing assemblies, and the Atomic.Events assembly itself.
        /// </summary>
        internal static bool ShouldRun(Compilation compilation)
        {
            // Skip in IDE (Rider/VS background analysis) — only run during actual builds
            if (!IsBuildTime)
                return false;

            // Skip the Atomic.Events assembly itself (it only defines [EventExtensionsAPI], doesn't use it)
            if (compilation.Assembly.Name == CodegenAssemblyName)
                return false;

            // Only run if the compilation references Atomic.Events (can use [EventExtensionsAPI])
            return compilation.ReferencedAssemblyNames.Any(n => n.Name == CodegenAssemblyName);
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Step 1: Find all candidate classes with [EventExtensionsAPI] attribute
            // Uses CreateSyntaxProvider for Unity 6000 (Roslyn 4.3.0) compatibility.
            var pipeline = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, _) => EventAPIParser.IsCandidate(node),
                transform: (ctx, _) => EventAPIParser.Transform(ctx)
            );

            // Step 2: Remove nulls (classes that failed semantic validation)
            var definitions = pipeline.Where(static def => def.HasValue)
                                      .Select(static (def, _) => def!.Value);

            // Step 3: Combine with compilation info and parse options
            var combined = definitions.Collect()
                .Combine(context.CompilationProvider)
                .Combine(context.ParseOptionsProvider);

            // Step 4: Generate source code for each definition
            context.RegisterSourceOutput(combined, (sourceProductionContext, tuple) =>
            {
                var ((defs, compilation), parseOptions) = tuple;

                // Setup debug output (reads ATOMIC_OUTPUT_SOURCEGEN_FILES define)
                SourceOutputHelpers.Setup(parseOptions);

                // Early bail-out if this compilation can't have [EventExtensionsAPI] classes
                if (!ShouldRun(compilation))
                    return;

                var assemblyName = compilation.Assembly.Name;

                foreach (var def in defs)
                {
                    using var logger = new DiagnosticLogger(sourceProductionContext, def.ClassName, Id);

                    try
                    {
                        string source = CodeEmitter.Emit(def);
                        string hintName = $"{def.ClassName}.g.cs";

                        sourceProductionContext.AddSource(hintName, source);

                        string busTypes = string.Join(", ", def.Events.Select(e => e.BusTypeName).Distinct());
                        SourceOutputHelpers.OutputSourceToFile(assemblyName, hintName, () => source);
                        SourceOutputHelpers.LogInfo($"Generated {assemblyName}/{hintName}: {def.Namespace}.{def.ClassName} → [{busTypes}] ({def.Events.Count} events)");

                        logger.LogInfo("EAPG0001", "EventAPIGenerator Trace",
                            $"Generated: {def.Namespace}.{def.ClassName} → [{busTypes}] ({def.Events.Count} events)");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        logger.LogError("EAPG0002", "EventAPIGenerator Internal Error",
                            $"Internal error: {exception}");
                    }
                }
            });
        }
    }
}

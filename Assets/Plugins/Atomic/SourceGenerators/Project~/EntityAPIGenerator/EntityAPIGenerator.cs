using System;
using System.Linq;
using System.Reflection;
using Atomic.SourceGenerators.Shared;
using EntityAPIGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EntityAPIGenerator
{
    /// <summary>
    /// Incremental source generator that reads <c>[EntityExtensionsAPI]</c>-marked classes
    /// and generates extension methods for Atomic entity value keys and tags.
    /// </summary>
    [Generator]
    public sealed class EntityAPIGenerator : IIncrementalGenerator
    {
        public const string Id = "EntityAPIGenerator";

        /// <summary>
        /// Name of the assembly that defines <c>[EntityExtensionsAPI]</c> attribute.
        /// </summary>
        internal static readonly string CodegenAssemblyName = "Atomic.Entities";

        /// <summary>
        /// <c>true</c> when running as part of a compiler invocation (not IDE analysis).
        /// In the IDE, source generators can run multiple times per keystroke;
        /// skipping there improves responsiveness. Generated types will still
        /// be available on the next actual build/domain reload.
        /// </summary>
        internal static readonly bool IsBuildTime = Assembly.GetEntryAssembly() != null;

        /// <summary>
        /// Determines whether this generator should run for the given compilation.
        /// Skips IDE analysis, non-referencing assemblies, and the Atomic.Entities assembly itself.
        /// </summary>
        internal static bool ShouldRun(Compilation compilation)
        {
            // Skip in IDE (Rider/VS background analysis) — only run during actual builds
            if (!IsBuildTime)
                return false;

            // Skip the Atomic.Entities assembly itself (it only defines [EntityExtensionsAPI], doesn't use it)
            if (compilation.Assembly.Name == CodegenAssemblyName)
                return false;

            // Only run if the compilation references Atomic.Entities (can use [EntityExtensionsAPI])
            return compilation.ReferencedAssemblyNames.Any(n => n.Name == CodegenAssemblyName);
        }

        /// <summary>
        /// Collects distinct entity type names used by value and tag fields.
        /// </summary>
        internal static System.Collections.Generic.IEnumerable<string> GetDistinctEntityTypes(EntityAPIDefinition def)
        {
            return def.Values.Select(v => v.EntityTypeName)
                .Concat(def.Tags.Select(t => t.EntityTypeName))
                .Distinct();
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Step 1: Find all candidate classes with [EntityExtensionsAPI] attribute
            // Uses CreateSyntaxProvider for Unity 6000 (Roslyn 4.3.0) compatibility.
            var pipeline = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, _) => EntityAPIParser.IsCandidate(node),
                transform: (ctx, _) => EntityAPIParser.Transform(ctx)
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

                // Early bail-out if this compilation can't have [EntityExtensionsAPI] classes
                if (!ShouldRun(compilation))
                    return;

                var assemblyName = compilation.Assembly.Name;

                // ── Process class-level [EntityExtensionsAPI] definitions ──
                foreach (var def in defs)
                {
                    // Accumulate all diagnostics per definition; reports on scope exit
                    using var logger = new DiagnosticLogger(sourceProductionContext, def.ClassName, Id);

                    try
                    {
                        string source = CodeEmitter.Emit(def);
                        string hintName = $"{def.ClassName}.g.cs";

                        sourceProductionContext.AddSource(hintName, source);

                        // Write to Temp/GeneratedCode/ for debugging (when ATOMIC_OUTPUT_SOURCEGEN_FILES is defined)
                        string entityTypes = string.Join(", ", GetDistinctEntityTypes(def));
                        SourceOutputHelpers.OutputSourceToFile(assemblyName, hintName, () => source);
                        SourceOutputHelpers.LogInfo($"Generated {assemblyName}/{hintName}: {def.Namespace}.{def.ClassName} → [{entityTypes}] ({def.Values.Count} values, {def.Tags.Count} tags, unsafe={def.Unsafe})");

                        logger.LogInfo("EAG0001", "EntityAPIGenerator Trace",
                            $"Generated: {def.Namespace}.{def.ClassName} → [{entityTypes}] " +
                            $"({def.Values.Count} values, {def.Tags.Count} tags, unsafe={def.Unsafe})");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        logger.LogError("EAG0002", "EntityAPIGenerator Internal Error",
                            $"Internal error: {exception}");
                    }
                }


            });
        }
    }
}

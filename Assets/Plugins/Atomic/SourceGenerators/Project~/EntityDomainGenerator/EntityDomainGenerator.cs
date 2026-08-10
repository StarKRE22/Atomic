using System;
using System.Linq;
using System.Reflection;
using Atomic.SourceGenerators.Shared;
using EntityDomainGenerator.Models;
using Microsoft.CodeAnalysis;

namespace EntityDomainGenerator
{
    /// <summary>
    /// Incremental source generator that reads <c>[EntityDomainBehaviours(typeof(TEntity))]</c>
    /// and generates strongly-typed domain behaviour interfaces for the target entity type.
    /// </summary>
    [Generator]
    public sealed class EntityDomainGenerator : IIncrementalGenerator
    {
        public const string Id = "EntityDomainGenerator";

        /// <summary>
        /// Name of the assembly that defines <c>[EntityDomainBehaviours]</c>.
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

            // Skip the Atomic.Entities assembly itself (it only defines the attribute)
            if (compilation.Assembly.Name == CodegenAssemblyName)
                return false;

            // Only run if the compilation references Atomic.Entities
            return compilation.ReferencedAssemblyNames.Any(n => n.Name == CodegenAssemblyName);
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Step 1: Find all candidate classes with [EntityDomainBehaviours] attribute.
            // Uses CreateSyntaxProvider for Unity 6000 (Roslyn 4.3.0) compatibility.
            var pipeline = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, _) => EntityDomainParser.IsCandidate(node),
                transform: (ctx, _) => EntityDomainParser.Transform(ctx)
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

                // Early bail-out if this compilation can't have [EntityDomainBehaviours] classes
                if (!ShouldRun(compilation))
                    return;

                var assemblyName = compilation.Assembly.Name;

                foreach (var def in defs)
                {
                    using var logger = new DiagnosticLogger(sourceProductionContext, def.ClassName, Id);

                    try
                    {
                        string source = EntityDomainEmitter.Emit(def);
                        string hintName = $"{def.ClassName}.{def.EntityShortName}.DomainBehaviours.g.cs";

                        sourceProductionContext.AddSource(hintName, source);

                        string behaviours = string.Join(", ", def.Behaviours.Select(b => def.EntityShortName + b.Suffix));
                        SourceOutputHelpers.OutputSourceToFile(assemblyName, hintName, () => source);
                        SourceOutputHelpers.LogInfo($"Generated {assemblyName}/{hintName}: {def.Namespace}.{def.ClassName} -> {def.EntityTypeName} [{behaviours}]");

                        logger.LogInfo("EDG0001", "EntityDomainGenerator Trace",
                            $"Generated: {def.Namespace}.{def.ClassName} -> {def.EntityTypeName} [{behaviours}]");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        logger.LogError("EDG0002", "EntityDomainGenerator Internal Error",
                            $"Internal error: {exception}");
                    }
                }
            });
        }
    }
}

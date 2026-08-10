using Atomic.SourceGenerators.Shared;
using EntityDomainGenerator.Models;

namespace EntityDomainGenerator
{
    /// <summary>
    /// Produces C# source code from <see cref="EntityDomainDefinition"/> models.
    /// Emits empty composite behaviour interfaces for a target entity type.
    /// </summary>
    internal static class EntityDomainEmitter
    {
        /// <summary>
        /// Generates the full C# source for a given <see cref="EntityDomainDefinition"/>.
        /// </summary>
        public static string Emit(EntityDomainDefinition def)
        {
            var w = new CodeWriter();

            // Header
            w.Line("/**");
            w.Line(" * Code generation. Don't modify!");
            w.Line(" **/");
            w.Line();

            // Usings
            w.Line("using Atomic.Entities;");

            // Namespace
            bool hasNamespace = !string.IsNullOrEmpty(def.Namespace);
            if (hasNamespace)
            {
                w.Line();
                w.Line($"namespace {def.Namespace}");
                w.Open();
            }

            foreach (var behaviour in def.Behaviours)
            {
                EmitInterface(ref w, def, behaviour);
            }

            if (hasNamespace) w.Close();

            return w.Result;
        }

        private static void EmitInterface(ref CodeWriter w, EntityDomainDefinition def, BehaviourInterface behaviour)
        {
            string interfaceName = $"{def.EntityShortName}{behaviour.Suffix}";

            w.Line();
            w.Line($"/// <summary>");
            w.Line($"/// Domain behaviour marker for <see cref=\"{def.EntityTypeName}\"/> {behaviour.Suffix.ToLowerInvariant()} logic.");
            w.Line($"/// </summary>");

            if (behaviour.Suffix == "Behaviour")
            {
                w.Line($"public interface {interfaceName} : {behaviour.BaseInterfaceName}");
            }
            else
            {
                string markerName = $"{def.EntityShortName}Behaviour";
                w.Line($"public interface {interfaceName} : {behaviour.BaseGenericInterfaceName}<{def.EntityTypeName}>, {markerName}");
            }

            w.Open();
            w.Close();
        }
    }
}

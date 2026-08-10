using System;
using System.Collections.Generic;

namespace EntityDomainGenerator.Models
{
    /// <summary>
    /// Describes a single class decorated with <c>[EntityDomainBehaviours(typeof(TEntity))]</c>
    /// and the behaviour interfaces to be generated for that entity type.
    /// Implements <see cref="IEquatable{T}"/> for incremental caching.
    /// </summary>
    public readonly struct EntityDomainDefinition : IEquatable<EntityDomainDefinition>
    {
        /// <summary>Full namespace of the annotated class.</summary>
        public string Namespace { get; }

        /// <summary>Name of the annotated class.</summary>
        public string ClassName { get; }

        /// <summary>Full display name of the target entity type (e.g. <c>MyNamespace.ISomeEntity</c>).</summary>
        public string EntityTypeName { get; }

        /// <summary>Short name of the target entity type used to construct generated interface names.</summary>
        public string EntityShortName { get; }

        /// <summary>Behaviour interface variants to generate for this domain.</summary>
        public IReadOnlyList<BehaviourInterface> Behaviours { get; }

        public EntityDomainDefinition(
            string ns,
            string className,
            string entityTypeName,
            string entityShortName,
            IReadOnlyList<BehaviourInterface> behaviours)
        {
            Namespace = ns;
            ClassName = className;
            EntityTypeName = entityTypeName;
            EntityShortName = entityShortName;
            Behaviours = behaviours;
        }

        public bool Equals(EntityDomainDefinition other) =>
            Namespace == other.Namespace &&
            ClassName == other.ClassName &&
            EntityTypeName == other.EntityTypeName &&
            EntityShortName == other.EntityShortName &&
            SequenceEqual(Behaviours, other.Behaviours);

        public override bool Equals(object obj) =>
            obj is EntityDomainDefinition other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Namespace?.GetHashCode() ?? 0);
            hash = hash * 31 + (ClassName?.GetHashCode() ?? 0);
            hash = hash * 31 + (EntityTypeName?.GetHashCode() ?? 0);
            hash = hash * 31 + (EntityShortName?.GetHashCode() ?? 0);
            foreach (var b in Behaviours)
                hash = hash * 31 + b.GetHashCode();
            return hash;
        }

        private static bool SequenceEqual<T>(IReadOnlyList<T> a, IReadOnlyList<T> b)
            where T : IEquatable<T>
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
                if (!a[i].Equals(b[i])) return false;
            return true;
        }
    }
}

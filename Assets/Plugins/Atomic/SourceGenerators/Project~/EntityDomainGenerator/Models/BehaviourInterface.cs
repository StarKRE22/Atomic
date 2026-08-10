using System;

namespace EntityDomainGenerator.Models
{
    /// <summary>
    /// Describes one generated domain behaviour interface.
    /// </summary>
    public readonly struct BehaviourInterface : IEquatable<BehaviourInterface>
    {
        /// <summary>Suffix appended to the entity short name to form the generated interface name.</summary>
        public string Suffix { get; }

        /// <summary>Base generic behaviour interface name (e.g. <c>IEntityTick</c>).</summary>
        public string BaseGenericInterfaceName { get; }

        /// <summary>
        /// Base non-generic behaviour interface name.
        /// For the root marker this is <c>IEntityBehaviour</c>; otherwise it matches
        /// <see cref="BaseGenericInterfaceName"/>.
        /// </summary>
        public string BaseInterfaceName { get; }

        public BehaviourInterface(string suffix, string baseGenericInterfaceName, string baseInterfaceName)
        {
            Suffix = suffix;
            BaseGenericInterfaceName = baseGenericInterfaceName;
            BaseInterfaceName = baseInterfaceName;
        }

        public bool Equals(BehaviourInterface other) =>
            Suffix == other.Suffix &&
            BaseGenericInterfaceName == other.BaseGenericInterfaceName &&
            BaseInterfaceName == other.BaseInterfaceName;

        public override bool Equals(object obj) =>
            obj is BehaviourInterface other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Suffix?.GetHashCode() ?? 0);
            hash = hash * 31 + (BaseGenericInterfaceName?.GetHashCode() ?? 0);
            hash = hash * 31 + (BaseInterfaceName?.GetHashCode() ?? 0);
            return hash;
        }
    }
}

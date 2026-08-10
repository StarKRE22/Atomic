using System;

namespace EntityAPIGenerator.Models
{
    /// <summary>
    /// Represents a <c>TagKey&lt;E&gt;</c> static field parsed from an <c>[EntityExtensionsAPI]</c> class,
    /// used to generate <c>HasXxxTag</c>, <c>AddXxxTag</c>, <c>DelXxxTag</c> extension methods.
    /// </summary>
    public readonly struct TagField : IEquatable<TagField>
    {
        /// <summary>Field name, e.g. <c>IsAlive</c>, <c>IsStunned</c>.</summary>
        public string Name { get; }

        /// <summary>Entity type name the tag extends, e.g. <c>IPlayerContext</c>.</summary>
        public string EntityTypeName { get; }

        public TagField(string name, string entityTypeName)
        {
            Name = name;
            EntityTypeName = entityTypeName;
        }

        public bool Equals(TagField other) =>
            Name == other.Name &&
            EntityTypeName == other.EntityTypeName;

        public override bool Equals(object obj) =>
            obj is TagField other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Name?.GetHashCode() ?? 0);
            hash = hash * 31 + (EntityTypeName?.GetHashCode() ?? 0);
            return hash;
        }
    }
}

using System;

namespace EntityAPIGenerator.Models
{
    /// <summary>
    /// Represents a <c>ValueKey&lt;E, T&gt;</c> field parsed from an <c>[EntityExtensionsAPI]</c> class.
    /// </summary>
    public readonly struct ValueField : IEquatable<ValueField>
    {
        /// <summary>Field name, e.g. <c>Camera</c>, <c>Mana</c>.</summary>
        public string Name { get; }

        /// <summary>Entity type name, e.g. <c>IPlayerContext</c>.</summary>
        public string EntityTypeName { get; }

        /// <summary>Value type full name, e.g. <c>Camera</c>, <c>IReactiveVariable&lt;int&gt;</c>.</summary>
        public string ValueTypeName { get; }

        /// <summary>
        /// Whether this specific field is marked <c>[Unsafe]</c>.
        /// Falls back to the class-level <c>Unsafe</c> flag during generation.
        /// </summary>
        public bool IsUnsafe { get; }

        public ValueField(string name, string entityTypeName, string valueTypeName, bool isUnsafe)
        {
            Name = name;
            EntityTypeName = entityTypeName;
            ValueTypeName = valueTypeName;
            IsUnsafe = isUnsafe;
        }

        public bool Equals(ValueField other) =>
            Name == other.Name &&
            EntityTypeName == other.EntityTypeName &&
            ValueTypeName == other.ValueTypeName &&
            IsUnsafe == other.IsUnsafe;

        public override bool Equals(object obj) =>
            obj is ValueField other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Name?.GetHashCode() ?? 0);
            hash = hash * 31 + (EntityTypeName?.GetHashCode() ?? 0);
            hash = hash * 31 + (ValueTypeName?.GetHashCode() ?? 0);
            hash = hash * 31 + IsUnsafe.GetHashCode();
            return hash;
        }
    }
}

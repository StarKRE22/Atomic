using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for singleton entities.
    /// Ensures a single globally accessible instance of type <typeparamref name="E"/>.
    /// Supports both default constructor and factory-based creation.
    /// </summary>
    /// <typeparam name="E">
    /// The concrete entity singleton type. 
    /// Must inherit from <see cref="EntitySingleton{E}"/> and provide either:
    /// a public parameterless constructor or a registered factory via <see cref="SetFactory"/>.
    /// </typeparam>
    public abstract class EntitySingleton<E> : Entity where E : EntitySingleton<E>, new()
    {
        private static E _instance;
        private static IEntityFactory<E> _factory;
        
        /// <summary>
        /// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tags">Optional collection of tag identifiers.</param>
        /// <param name="values">Optional collection of key-value pairs.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// The constructor initializes internal capacities based on the provided collections,
        /// then adds all specified tags, values, and behaviours immediately.
        /// </remarks>
        protected EntitySingleton(
            string name,
            IEnumerable<string> tags,
            IEnumerable<KeyValuePair<string, object>> values,
            IEnumerable<IEntityBehaviour> behaviours,
            Settings? settings = null
        ) : base(name, tags, values, behaviours, settings)
        {
        }

        /// <summary>
        /// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tags">Optional collection of tag identifiers.</param>
        /// <param name="values">Optional collection of key-value pairs.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// The constructor initializes internal capacities based on the provided collections,
        /// then adds all specified tags, values, and behaviours immediately.
        /// </remarks>
        protected EntitySingleton(
            string name,
            IEnumerable<int> tags,
            IEnumerable<KeyValuePair<int, object>> values,
            IEnumerable<IEntityBehaviour> behaviours,
            Settings? settings = null
        ) : base(name, tags, values, behaviours, settings)
        {
        }

        /// <summary>
        /// Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
        /// </summary>
        /// <param name="name">The name of the entity. If <c>null</c>, an empty string is used.</param>
        /// <param name="tagCapacity">Initial capacity for tag storage to minimize memory allocations.</param>
        /// <param name="valueCapacity">Initial capacity for value storage to minimize memory allocations.</param>
        /// <param name="behaviourCapacity">Initial capacity for behaviour storage to minimize memory allocations.</param>
        /// <param name="settings">Optional entity settings. If <c>null</c>, <see cref="Settings.disposeValues"/> defaults to <c>true</c>.</param>
        /// <remarks>
        /// Pre-allocates internal structures for efficient usage and registers the entity in <see cref="EntityRegistry"/>.
        /// </remarks>
        protected EntitySingleton(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0,
            Settings? settings = null
        ) : base(name, tagCapacity, valueCapacity, behaviourCapacity, settings)
        {
        }
        
        /// <summary>
        /// Gets the global singleton instance of type <typeparamref name="E"/>.
        /// <para>
        /// If a factory is registered via <see cref="SetFactory"/>, the instance is created using that factory.
        /// Otherwise, the instance is created using the parameterless constructor (<c>new()</c>).
        /// </para>
        /// <para>
        /// The same instance is returned on every access until disposed via <see cref="DropInstance"/>.
        /// </para>
        /// </summary>
        public static E Instance
        {
            get { return _instance ??= _factory != null ? _factory.Create() : new E(); }
        }

        /// <summary>
        /// Registers a custom factory method for creating the singleton instance.
        /// <para>
        /// This method must be called before the first access to <see cref="Instance"/>.
        /// </para>
        /// </summary>
        /// <param name="factory">
        /// The factory that will be used to create new instances of <typeparamref name="E"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is <c>null</c>.</exception>
        public static void SetFactory(IEntityFactory<E> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Drops the current singleton instance, if it exists, 
        /// and resets the internal reference to <c>null</c>.
        /// <para>
        /// After calling this method, the next access to <see cref="Instance"/> will create a new instance
        /// (using either the registered factory or the default constructor).
        /// </para>
        /// </summary>
        public static void DropInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        /// <summary>
        /// Drops the current singleton instance (if any) and immediately creates a new one.
        /// <para>
        /// This is equivalent to calling <see cref="DropInstance"/> followed by accessing <see cref="Instance"/>.
        /// </para>
        /// </summary>
        /// <returns>
        /// A newly created singleton instance of type <typeparamref name="E"/>.
        /// </returns>
        public static E ResetInstance()
        {
            DropInstance();
            return Instance;
        }
    }
}
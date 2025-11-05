#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        [Serializable]
        public struct CreateArgs
        {
            public string name;
            public IEnumerable<int> tags;
            public IReadOnlyDictionary<int, object> values;
            public IEnumerable<IEntityBehaviour> behaviours;
            public List<SceneEntityInstaller> sceneInstallers;
            public List<ScriptableEntityInstaller> scriptableInstallers;
            public List<SceneEntity> children;

            public int initialTagCapacity;
            public int initialValueCapacity;
            public int initialBehaviourCapacity;

            public bool installOnAwake;
            public bool uninstallOnDestroy;
            public bool disposeValues;
            public bool useUnityLifecycle;
        }

        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> GameObject and configures it with optional tags, values, and behaviours.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SceneEntity Create(in CreateArgs args) => Create<SceneEntity>(in args);

        /// <summary>
        /// Creates a new SceneEntity of type <typeparamref name="E"/> and configures it with optional tags, values, behaviours, and other initialization options.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to create.</typeparam>
        /// <param name="args">A <see cref="CreateArgs"/> structure containing configuration options for the SceneEntity.</param>
        /// <returns>The newly created SceneEntity of type <typeparamref name="E"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Create<E>(in CreateArgs args) where E : SceneEntity
        {
            GameObject gameObject = new GameObject();
            gameObject.SetActive(false);

            E entity = gameObject.AddComponent<E>();
            entity.name = args.name;

            entity.sceneInstallers = args.sceneInstallers;
            entity.scriptableInstallers = args.scriptableInstallers;
            entity.childInstallers = args.children;

            entity.installOnAwake = args.installOnAwake;
            entity.uninstallOnDestroy = args.uninstallOnDestroy;
            entity.disposeValues = args.disposeValues;
            entity.useUnityLifecycle = args.useUnityLifecycle;

            entity.initialBehaviourCapacity = Mathf.Max(1, args.initialBehaviourCapacity);
            entity.initialTagCapacity = Mathf.Max(1, args.initialTagCapacity);
            entity.initialValueCapacity = Mathf.Max(1, args.initialValueCapacity);

            entity.Construct();

            entity.AddTags(args.tags);
            entity.AddValues(args.values);
            entity.AddBehaviours(args.behaviours);

            entity.Register();
            
            gameObject.SetActive(true);
            return entity;
        }

        /// <summary>
        /// Creates a new SceneEntity of type <typeparamref name="E"/> with optional configuration parameters.
        /// This overload constructs a <see cref="CreateArgs"/> object internally.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to create.</typeparam>
        /// <param name="name">Optional name for the GameObject.</param>
        /// <param name="tags">Optional collection of integer tags to assign to the entity.</param>
        /// <param name="values">Optional dictionary of key-value pairs to assign to the entity.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach to the entity.</param>
        /// <param name="installOnAwake">If true, installers will be run on Awake.</param>
        /// <param name="disposeValues">If true, values will be disposed when the entity is destroyed.</param>
        /// <param name="useUnityLifecycle">If true, the entity will use Unity's lifecycle methods.</param>
        /// <param name="initialTagCount">Initial capacity for tags.</param>
        /// <param name="initialValueCount">Initial capacity for values.</param>
        /// <param name="initialBehaviourCount">Initial capacity for behaviours.</param>
        /// <returns>The newly created SceneEntity of type <typeparamref name="E"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Create<E>(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null,
            bool installOnAwake = true,
            bool uninstallOnDestroy = true,
            bool disposeValues = true,
            bool useUnityLifecycle = true,
            int initialTagCount = 1,
            int initialValueCount = 1,
            int initialBehaviourCount = 1
        ) where E : SceneEntity => Create<E>(new CreateArgs
        {
            name = name,
            tags = tags,
            values = values,
            behaviours = behaviours,
            installOnAwake = installOnAwake,
            uninstallOnDestroy = uninstallOnDestroy,
            disposeValues = disposeValues,
            useUnityLifecycle = useUnityLifecycle,
            initialTagCapacity = initialTagCount,
            initialValueCapacity = initialValueCount,
            initialBehaviourCapacity = initialBehaviourCount
        });

        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> with optional configuration parameters.
        /// This overload constructs a <see cref="CreateArgs"/> object internally and returns a non-generic SceneEntity.
        /// </summary>
        /// <param name="name">Optional name for the GameObject.</param>
        /// <param name="tags">Optional collection of integer tags to assign to the entity.</param>
        /// <param name="values">Optional dictionary of key-value pairs to assign to the entity.</param>
        /// <param name="behaviours">Optional collection of behaviours to attach to the entity.</param>
        /// <param name="installOnAwake">If true, installers will be run on Awake.</param>
        /// <param name="uninstallOnDestroy">If true, installers will be run on Awake.</param>
        /// <param name="disposeValues">If true, values will be disposed when the entity is destroyed.</param>
        /// <param name="useUnityLifecycle">If true, the entity will use Unity's lifecycle methods.</param>
        /// <param name="initialTagCount">Initial capacity for tags.</param>
        /// <param name="initialValueCount">Initial capacity for values.</param>
        /// <param name="initialBehaviourCount">Initial capacity for behaviours.</param>
        /// <returns>The newly created <see cref="SceneEntity"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SceneEntity Create(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null,
            bool installOnAwake = true,
            bool uninstallOnDestroy = true,
            bool disposeValues = true,
            bool useUnityLifecycle = true,
            int initialTagCount = 1,
            int initialValueCount = 1,
            int initialBehaviourCount = 1
        ) => Create(new CreateArgs
        {
            name = name,
            tags = tags,
            values = values,
            behaviours = behaviours,
            installOnAwake = installOnAwake,
            uninstallOnDestroy = uninstallOnDestroy,
            disposeValues = disposeValues,
            useUnityLifecycle = useUnityLifecycle,
            initialTagCapacity = initialTagCount,
            initialValueCapacity = initialValueCount,
            initialBehaviourCapacity = initialBehaviourCount
        });

        /// <summary>
        /// Instantiates a prefab and installs the resulting <see cref="SceneEntity"/> under the specified parent.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SceneEntity Create(SceneEntity prefab, Transform parent = null) =>
            Create(prefab, Vector3.zero, Quaternion.identity, parent);

        /// <summary>
        /// Instantiates a new SceneEntity of type <typeparamref name="E"/> from the given prefab, optionally setting its parent transform.
        /// The entity is positioned at <see cref="Vector3.zero"/> and rotated with <see cref="Quaternion.identity"/> by default.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to create.</typeparam>
        /// <param name="prefab">The prefab instance of <typeparamref name="E"/> to instantiate.</param>
        /// <param name="parent">Optional parent <see cref="Transform"/> under which the new entity will be placed. Defaults to <c>null</c>.</param>
        /// <returns>The newly instantiated SceneEntity of type <typeparamref name="E"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Create<E>(E prefab, Transform parent = null) where E : SceneEntity =>
            Create(prefab, Vector3.zero, Quaternion.identity, parent);

        /// <summary>
        /// Instantiates a prefab at the given position and rotation with optional parent, then installs it.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SceneEntity Create(
            SceneEntity prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) => Create<SceneEntity>(prefab, position, rotation, parent);

        /// <summary>
        /// Instantiates a new SceneEntity of type <typeparamref name="E"/> from the given prefab at the position and rotation of a reference transform,
        /// optionally assigning a parent transform.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to create.</typeparam>
        /// <param name="prefab">The prefab instance of <typeparamref name="E"/> to instantiate.</param>
        /// <param name="point">The reference <see cref="Transform"/> from which the position and rotation are copied.</param>
        /// <param name="parent">Optional parent <see cref="Transform"/> under which the new entity will be placed. Defaults to <c>null</c>.</param>
        /// <returns>The newly instantiated SceneEntity of type <typeparamref name="E"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Create<E>(E prefab, Transform point, Transform parent) where E : SceneEntity
        {
            E entity = Instantiate(prefab, point.position, point.rotation, parent);
            entity.Register();
            entity.Install();
            return entity;
        }

        /// <summary>
        /// Instantiates a new SceneEntity of type <typeparamref name="E"/> from the given prefab at a specific position and rotation,
        /// optionally assigning a parent transform.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to create.</typeparam>
        /// <param name="prefab">The prefab instance of <typeparamref name="E"/> to instantiate.</param>
        /// <param name="position">The position at which to place the new entity.</param>
        /// <param name="rotation">The rotation to apply to the new entity.</param>
        /// <param name="parent">Optional parent <see cref="Transform"/> under which the new entity will be placed. Defaults to <c>null</c>.</param>
        /// <returns>The newly instantiated SceneEntity of type <typeparamref name="E"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Create<E>(
            E prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) where E : SceneEntity
        {
            E entity = Instantiate(prefab, position, rotation, parent);
            entity.Register();
            entity.Install();
            return entity;
        }
    }
}
#endif
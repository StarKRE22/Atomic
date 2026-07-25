using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using UnityEngine.SceneManagement;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides extension methods for <see cref="IEntity"/> to simplify operations such as adding/removing tags, values, and behaviours.
    /// </summary>
    public static partial class Extensions
    {
        #region Clearing

        /// <summary>
        /// Clears all data (tags, values, behaviours) from this entity.
        /// </summary>
        public static void Clear(this IEntity entity)
        {
            entity.ClearTags();
            entity.ClearValues();
            entity.ClearBehaviours();
        }

        #endregion
        
        #region Installing

        /// <summary>
        /// Installs logic from a single <see cref="IEntityInstaller"/> into the specified entity.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="installer">The installer that provides logic to install.</param>
        /// <returns>The same <paramref name="entity"/> after installation for chaining.</returns>
        /// <remarks>
        /// This method delegates the installation process to the <see cref="IEntityInstaller.Install(IEntity)"/> method.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEntity Install(this IEntity entity, IEntityInstaller installer)
        {
            installer.Install(entity);
            return entity;
        }

        /// <summary>
        /// Installs logic from multiple <see cref="IEntityInstaller"/> instances into the specified entity.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="installers">A collection of installers. Can be <c>null</c>, in which case nothing is installed.</param>
        /// <remarks>
        /// Each installer in <paramref name="installers"/> will have its <see cref="IEntityInstaller.Install(IEntity)"/> method invoked.
        /// </remarks>
        public static void Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)
        {
            if (installers == null)
                return;

            foreach (IEntityInstaller installer in installers)
                installer.Install(entity);
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Installs logic from all <see cref="MonoEntityInstaller"/> components found in the specified scene.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="scene">The scene in which to search for installers.</param>
        /// <param name="includeInactive">
        /// If <c>true</c>, installers on inactive GameObjects will also be included; otherwise only active installers are considered.
        /// </param>
        /// <remarks>
        /// This method iterates over all root GameObjects in the scene and applies each found <see cref="MonoEntityInstaller"/> to the entity.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, goCount = gameObjects.Length; g < goCount; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<MonoEntityInstaller>(includeInactive);
                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    MonoEntityInstaller installer = installers[i];
                    installer.Install(entity);
                }
            }
        }

        /// <summary>
        /// Installs logic from all <see cref="MonoEntityInstaller{E}"/> components found in the specified scene for a generic entity type.
        /// </summary>
        /// <typeparam name="T">The entity type that implements <see cref="IEntity"/>.</typeparam>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="scene">The scene in which to search for installers.</param>
        /// <param name="includeInactive">
        /// If <c>true</c>, installers on inactive GameObjects will also be included; otherwise only active installers are considered.
        /// </param>
        /// <remarks>
        /// This method iterates over all root GameObjects in the scene and applies each found <see cref="MonoEntityInstaller{E}"/> to the entity.
        /// Useful for generic entities or strongly-typed scenarios.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)
            where T : class, IEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, count = gameObjects.Length; g < count; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<MonoEntityInstaller<T>>(includeInactive);

                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    MonoEntityInstaller<T> installer = installers[i];
                    installer.Install(entity);
                }
            }
        }
#endif

        #endregion
        
        #region GetEntity

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this GameObject gameObject, out IEntity entity) =>
            gameObject.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity entity) =>
            component.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2D, out IEntity entity) =>
            collision2D.gameObject.TryGetComponent(out entity);
#endif

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity entity) =>
            collision.gameObject.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this GameObject gameObject, out IEntity entity)
        {
            entity = gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Component component, out IEntity entity)
        {
            entity = component.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision2D collision2D, out IEntity entity)
        {
            entity = collision2D.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision collision, out IEntity entity)
        {
            entity = collision.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawGizmos(this IEntity entity)
        {
            for (int i = 0; i < entity.BehaviourCount; i++)
                if (entity.GetBehaviourAt(i) is IEntityGizmos gizmos) 
                    gizmos.DrawGizmos(entity);
        }
    }
}
#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        /// <summary>
        /// Indicates whether this entity has already been installed.
        /// </summary>
        public bool Installed => _installed;

        private bool _installed;

        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;

            this.OnInstall();

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity child = this.children[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }

            _installed = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnInstall()
        {
        }

        /// <summary>
        /// Uninstalls all configured installers and child entities from this SceneEntity.
        /// Marks the entity as not installed, allowing it to be reinstalled.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Uninstall()
        {
            if (!_installed)
                return;

            this.OnUninstall();

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer != null)
                        installer.Uninstall(this);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity child = this.children[i];
                    if (child != null)
                        child.Uninstall();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }

            _installed = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnUninstall()
        {
        }
        
        /// <summary>
        /// Installs all <see cref="SceneEntity"/> instances found in the given scene that are not yet installed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll(Scene scene) => InstallAll<SceneEntity>(scene);

        /// <summary>
        /// Installs all <see cref="SceneEntity"/> instances of type <typeparamref name="E"/> found in the specified <see cref="Scene"/> 
        /// that are not yet installed.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to search for and install.</typeparam>
        /// <param name="scene">The scene in which to search for <typeparamref name="E"/> instances.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll<E>(Scene scene) where E : SceneEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, gameObjectCount = gameObjects.Length; g < gameObjectCount; g++)
            {
                GameObject gameObject = gameObjects[g];
                E[] entities = gameObject.GetComponentsInChildren<E>();
                for (int e = 0, entityCount = entities.Length; e < entityCount; e++)
                {
                    E entity = entities[e];
                    if (!entity.Installed)
                        entity.Install();
                }
            }
        }
    }
}
#endif
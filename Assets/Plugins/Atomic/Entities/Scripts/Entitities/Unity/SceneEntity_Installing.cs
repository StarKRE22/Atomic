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
            
            if (this.scriptableInstallers != null)
            {
                for (int i = 0, count = this.scriptableInstallers.Count; i < count; i++)
                {
                    ScriptableEntityInstaller installer = this.scriptableInstallers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(ScriptableEntityInstaller)} at index {i}!",
                            this);
                }
            }

            if (this.sceneInstallers != null)
            {
                for (int i = 0, count = this.sceneInstallers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.sceneInstallers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(SceneEntityInstaller)} at index {i}!",
                            this);
                }
            }
            
            this.OnInstall();

            if (this.childInstallers != null)
            {
                for (int i = 0, count = this.childInstallers.Count; i < count; i++)
                {
                    SceneEntity child = this.childInstallers[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning($"SceneEntity {this.name}: Ops! Detected missing child entity at index {i}!",
                            this);
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
            
            if (this.childInstallers != null)
            {
                for (int i = 0, count = this.childInstallers.Count; i < count; i++)
                {
                    SceneEntity child = this.childInstallers[i];
                    if (child != null)
                        child.Uninstall();
                    else
                        Debug.LogWarning($"SceneEntity {this.name}: Ops! Detected missing child entity at index {i}!",
                            this);
                }
            }

            this.OnUninstall();

            if (this.sceneInstallers != null)
            {
                for (int i = 0, count = this.sceneInstallers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.sceneInstallers[i];
                    if (installer != null)
                        installer.Uninstall(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(SceneEntityInstaller)} at index {i}!",
                            this);
                }
            }

            if (this.scriptableInstallers != null)
            {
                for (int i = 0, count = this.scriptableInstallers.Count; i < count; i++)
                {
                    ScriptableEntityInstaller installer = this.scriptableInstallers[i];
                    if (installer != null)
                        installer.Uninstall(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(ScriptableEntityInstaller)} at index {i}!",
                            this);
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
#if UNITY_5_3_OR_NEWER
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    public partial class MonoEntity
    {
        /// <summary>
        /// Indicates whether this entity has already been installed.
        /// </summary>
        public bool Installed => _installed;

        private bool _installed;
        private event Action _onInstalled;
        private event Action _onUninstalled;

        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;

            _installed = true;
            
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
                    MonoEntityInstaller installer = this.sceneInstallers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(MonoEntityInstaller)} at index {i}!",
                            this);
                }
            }

            this.OnInstall();
            _onInstalled?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnInstall()
        {
        }

        public void WhenInstall(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_installed)
                action.Invoke();
            else
                _onInstalled += action;
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

            _installed = false;
            this.OnUninstall();

            if (this.sceneInstallers != null)
            {
                for (int i = 0, count = this.sceneInstallers.Count; i < count; i++)
                {
                    MonoEntityInstaller installer = this.sceneInstallers[i];
                    if (installer != null)
                        installer.Uninstall(this);
                    else
                        Debug.LogWarning(
                            $"SceneEntity {this.name}: Ops! Detected missing {nameof(MonoEntityInstaller)} at index {i}!",
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

            _onUninstalled?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnUninstall()
        {
        }

        public void WhenUninstall(Action action)
        {
            _onUninstalled += action ?? throw new ArgumentNullException(nameof(action));
        }
        
        /// <summary>
        /// Installs all <see cref="MonoEntity"/> instances found in the given scene that are not yet installed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll(Scene scene, bool includeInactive = false)
        {
            InstallAll<MonoEntity>(scene, includeInactive);
        }

        /// <summary>
        /// Installs all <see cref="MonoEntity"/> instances of type <typeparamref name="E"/> found in the specified <see cref="Scene"/> 
        /// that are not yet installed.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="MonoEntity"/> to search for and install.</typeparam>
        /// <param name="scene">The scene in which to search for <typeparamref name="E"/> instances.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll<E>(Scene scene, bool includeInactive = false) where E : MonoEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, gameObjectCount = gameObjects.Length; g < gameObjectCount; g++)
            {
                GameObject gameObject = gameObjects[g];
                E[] entities = gameObject.GetComponentsInChildren<E>(includeInactive);
                for (int e = 0, entityCount = entities.Length; e < entityCount; e++)
                {
                    E entity = entities[e];
                    if (!entity.Installed  && (entity.gameObject.activeInHierarchy || includeInactive)) 
                        entity.Install();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll(Component component, bool includeInactive = false)
        {
            MonoEntity[] entities = component.GetComponentsInChildren<MonoEntity>(includeInactive);
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                MonoEntity entity = entities[i];
                if (!entity.Installed  && (entity.gameObject.activeInHierarchy || includeInactive)) 
                    entity.Install();
            }
        }
    }
}
#endif
#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;
using UnityEngine;

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
    }
}
#endif
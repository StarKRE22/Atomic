#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides editor-time lifecycle support for the <see cref="SceneEntity"/>,
    /// including auto-refresh, edit-mode installation, and simulated lifecycle events.
    /// </summary>
    public partial class SceneEntity<E>
    {
        /// <summary>
        /// Automatically gathers installers and child entities when the component is reset.
        /// </summary>
        private void Reset()
        {
            this.installers = new List<SceneEntityInstaller<E>>(this.GetComponentsInChildren<SceneEntityInstaller<E>>());
            this.children = new List<SceneEntity<E>>(this.GetComponentsInChildren<SceneEntity<E>>());
            this.children.Remove(this);
        }

        /// <summary>
        /// Called when the script is loaded or a value is changed in the Inspector. Triggers auto-refresh if enabled.
        /// </summary>
        private void OnValidate() => this.AutoRefresh();

        /// <summary>
        /// Automatically refreshes the entity in Edit Mode if <c>installInEditMode</c> is true.
        /// </summary>
        private void AutoRefresh()
        {
            if (!this.installInEditMode)
                return;

            if (EditorApplication.isPlaying || EditorApplication.isCompiling)
                return;

            try
            {
                this.SetRefreshCallbackToInstallers();
                this.RefreshInEditMode();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Sets refresh callbacks on all associated installers.
        /// </summary>
        private void SetRefreshCallbackToInstallers()
        {
            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneEntityInstaller<E> installer = this.installers[i];
                if (installer != null)
                    installer.refreshCallback = this.RefreshInEditMode;
            }
        }

        /// <summary>
        /// Refreshes the entity's state in the Unity Editor, simulating the full entity lifecycle.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Test Install"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        private void RefreshInEditMode()
        {
            this.ResetInstalledFlag();

            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;
            if (!isPrefab)
            {
                this.DisableInEditMode();
                this.DisposeInEditMode();
            }

            this.Install();
            this.Precompile();

            if (!isPrefab)
            {
                this.InitInEditMode();
                this.EnableInEditMode();
            }
        }

        /// <summary>
        /// Simulates initialization of all entity behaviours in Edit Mode.
        /// </summary>
        private void InitInEditMode()
        {
            if (this.Initialized)
                return;

            foreach (IBehaviour<E> behaviour in GetBehaviours())
                if (behaviour is IInit<E> dispose && IsEditModeSupported(behaviour))
                    dispose.Init(this);
        }

        /// <summary>
        /// Simulates enabling of all entity behaviours in Edit Mode.
        /// </summary>
        private void EnableInEditMode()
        {
            if (this.Enabled)
                return;

            foreach (IBehaviour<E> behaviour in this.GetBehaviours())
                if (behaviour is IEnable<E> dispose && IsEditModeSupported(behaviour))
                    dispose.Enable(this);
        }

        /// <summary>
        /// Simulates disabling of all entity behaviours in Edit Mode.
        /// </summary>
        private void DisableInEditMode()
        {
            if (this is not {Enabled: true})
                return;

            foreach (IBehaviour<E> behaviour in this.GetBehaviours())
                if (behaviour is IDisable<E> disable && IsEditModeSupported(behaviour))
                    disable.Disable(this);
        }

        /// <summary>
        /// Simulates disposal of all entity behaviours in Edit Mode.
        /// </summary>
        private void DisposeInEditMode()
        {
            if (this is not {Initialized: true})
                return;

            foreach (IBehaviour<E> behaviour in this.GetBehaviours())
            {
                if (behaviour is IDispose<E> dispose && IsEditModeSupported(behaviour))
                    dispose.Dispose(this);
            }
        }

        /// <summary>
        /// Checks whether a behaviour is marked to support edit mode lifecycle.
        /// </summary>
        private static bool IsEditModeSupported(IBehaviour<E> behaviour) => 
            behaviour.GetType().IsDefined(typeof(EditModeBehaviourAttribute));
    }
}

#endif
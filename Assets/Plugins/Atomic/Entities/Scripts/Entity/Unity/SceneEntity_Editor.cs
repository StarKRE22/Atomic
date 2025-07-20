#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using static Atomic.Entities.InternalUtils;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides editor-time lifecycle support for the <see cref="SceneEntity"/>,
    /// including auto-refresh, edit-mode installation, and simulated lifecycle events.
    /// </summary>
    public partial class SceneEntity
    {
        /// <summary>
        /// Automatically gathers installers and child entities when the component is reset.
        /// </summary>
        private void Reset()
        {
            this.installers = new List<SceneInstaller>(this.GetComponentsInChildren<SceneInstaller>());
            this.children = new List<SceneEntity>(this.GetComponentsInChildren<SceneEntity>());
            this.children.Remove(this);
        }

        /// <summary>
        /// Automatically refreshes the entity in Edit Mode if <c>installInEditMode</c> is true.
        /// </summary>
        private void OnValidate()
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
                SceneInstaller installer = this.installers[i];
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
            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;

            if (!isPrefab)
            {
                this.DisableInEditMode();
                this.DisposeInEditMode();
            }

            this.MarkAsNotInstalled();
            this.Install();
            this.Precompile();

            if (!isPrefab)
            {
                this.InitInEditMode();
                this.EnableInEditMode();
            }
        }
        
        /// <summary>
        /// Precompiles current capacities from the entity and stores them into serialized fields
        /// for inspection and editor-time optimization.
        /// </summary>
        private void Precompile()
        {
            _initialTagCapacity = _tagCount;
            _initialValueCapacity = _valueCount;
            _initialBehaviourCapacity = _behaviourCount;
        }

        /// <summary>
        /// Simulates initialization of all entity behaviours in Edit Mode.
        /// </summary>
        private void InitInEditMode()
        {
            if (_initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
            {
                IEntityBehaviour behaviour = _behaviours[i];
                if (behaviour is IEntityInit dispose && IsExecuteInEditModeDefined(behaviour))
                    dispose.Init(this);
            }
        }

        /// <summary>
        /// Simulates enabling of all entity behaviours in Edit Mode.
        /// </summary>
        private void EnableInEditMode()
        {
            if (_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
            {
                IEntityBehaviour behaviour = _behaviours[i];
                if (behaviour is IEntityEnable dispose && IsExecuteInEditModeDefined(behaviour))
                    dispose.Enable(this);
            }
        }

        /// <summary>
        /// Simulates disabling of all entity behaviours in Edit Mode.
        /// </summary>
        private void DisableInEditMode()
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
            {
                IEntityBehaviour behaviour = _behaviours[i];
                if (behaviour is IEntityDisable disable && IsExecuteInEditModeDefined(behaviour))
                    disable.Disable(this);
            }
        }

        /// <summary>
        /// Simulates disposal of all entity behaviours in Edit Mode.
        /// </summary>
        private void DisposeInEditMode()
        {
            if (!_initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
            {
                IEntityBehaviour behaviour = _behaviours[i];
                if (behaviour is IEntityDispose dispose && IsExecuteInEditModeDefined(behaviour))
                    dispose.Dispose(this);
            }
        }
    }
}

#endif
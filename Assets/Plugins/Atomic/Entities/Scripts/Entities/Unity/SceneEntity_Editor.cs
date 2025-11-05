#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
        /// Compiles the entity's state in the Unity Editor, simulating the full entity lifecycle.
        /// </summary>
#if ODIN_INSPECTOR
        [Button("Compile"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceBefore = 4)]
        [PropertyTooltip(
            "Compiles the entity's state in the Unity Editor, simulating installing, precomputing, and lifecycle")]
#endif
        [ContextMenu(nameof(Compile))]
        private void Compile()
        {
            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;

            try
            {
                if (!isPrefab)
                {
                    this.DisableInEditMode();
                    this.DisposeInEditMode();
                }

                this.Uninstall();
                this.Construct();
                this.Install();
                this.PrecomputeCapacities();

                if (!isPrefab)
                {
                    this.InitInEditMode();
                    this.EnableInEditMode();
                }

                // Debug.Log($"<color=#00D4FF>{this.name} Compilation completed successfully!</color>", this);
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=#FF3C3C>{this.name} Compilation failed: {e.Message}</color>\n{e.StackTrace}", this);
            }
        }
        
        /// <summary>
        /// Sets refresh callbacks on all associated installers.
        /// </summary>
        private void SetRefreshCallbackToInstallers()
        {
            for (int i = 0, count = this.sceneInstallers.Count; i < count; i++)
            {
                SceneEntityInstaller installer = this.sceneInstallers[i];
                if (installer != null)
                    installer.refreshCallback = this.Compile;
            }
        }


        /// <summary>
        /// Precompute current capacities from the entity and stores them into serialized fields
        /// for inspection and editor-time optimization.
        /// </summary>
        private void PrecomputeCapacities()
        {
            this.initialTagCapacity = _tagCount;
            this.initialValueCapacity = _valueCount;
            this.initialBehaviourCapacity = _behaviourCount;
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
                if (behaviour is IEntityInit dispose && IsRunInEditModeDefined(behaviour))
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
                if (behaviour is IEntityEnable dispose && IsRunInEditModeDefined(behaviour))
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
                if (behaviour is IEntityDisable disable && IsRunInEditModeDefined(behaviour))
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
                if (behaviour is IEntityDispose dispose && IsRunInEditModeDefined(behaviour))
                    dispose.Dispose(this);
            }
        }

        /// <summary>
        /// Automatically compiles the entity in Edit Mode if <c>autoCompile</c> is true.
        /// </summary>
        private void OnValidate()
        {
            if (!this.autoCompile)
                return;

            if (EditorApplication.isPlaying || EditorApplication.isCompiling)
                return;

            try
            {
                this.SetRefreshCallbackToInstallers();
                this.Compile();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Automatically gathers installers and child entities when the component is reset.
        /// </summary>

#if ODIN_INSPECTOR
        [Button(nameof(Reset)), HideInPlayMode]
        [GUIColor(1f, 0.92f, 0.02f)]
        [PropertySpace(SpaceBefore = 4, SpaceAfter = 8)]
        [PropertyTooltip("Automatically gathers installers and child entities and resets to default state")]
#endif
        private protected virtual void Reset()
        {
            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;

            try
            {
                if (!isPrefab)
                {
                    this.DisableInEditMode();
                    this.DisposeInEditMode();
                }

                this.Uninstall();
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=#FF3C3C>{this.name} Reset failed: {e.Message}</color>\n{e.StackTrace}", this);
            }

            //Reset lifecycle:
            this.useUnityLifecycle = true;
            this.disposeValues = true;

            //Reset installing:
            this.installOnAwake = true;
            this.uninstallOnDestroy = true;
            this.sceneInstallers = new List<SceneEntityInstaller>(this.GetComponentsInChildren<SceneEntityInstaller>());
            this.scriptableInstallers = new List<ScriptableEntityInstaller>();
            this.childInstallers = new List<SceneEntity>(this.GetComponentsInChildren<SceneEntity>());
            this.childInstallers.Remove(this);

            //Reset gizmos:
            this.onlySelectedGizmos = false;
            this.onlyEditModeGizmos = false;

            //Reset editor:
            this.autoCompile = false;

            //Reset optimization:
            this.initialTagCapacity = 1;
            this.initialValueCapacity = 1;
            this.initialBehaviourCapacity = 0;
            this.Construct();

            // Debug.Log($"<color=#FFEB04>{this.name} Reset completed successfully!</color>", this);
        }
    }
}

#endif
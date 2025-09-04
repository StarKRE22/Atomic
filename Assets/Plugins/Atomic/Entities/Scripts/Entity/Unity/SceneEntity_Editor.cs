#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.EntityUtils_Internal;

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

#if ODIN_INSPECTOR
        // [FoldoutGroup("Debug")]
        [PropertyOrder(96)]
        [Button(nameof(Reset)), HideInPlayMode]
        [GUIColor(1f, 0.92f, 0.02f)]
#endif
        private void Reset()
        {
            this.installers = new List<SceneEntityInstaller>(this.GetComponentsInChildren<SceneEntityInstaller>());
            this.children = new List<SceneEntity>(this.GetComponentsInChildren<SceneEntity>());
            this.children.Remove(this);
            Debug.Log($"<color=#FFEB04>{this.name} Reset successfully!</color>", this);
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
                this.Compile();
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
                SceneEntityInstaller installer = this.installers[i];
                if (installer != null)
                    installer.refreshCallback = this.Compile;
            }
        }

        /// <summary>
        /// Refreshes the entity's state in the Unity Editor, simulating the full entity lifecycle.
        /// </summary>
#if ODIN_INSPECTOR
        [PropertyOrder(95)]
        [Button("Compile"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8)]
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

                this.Construct();
                this.MarkAsNotInstalled();
                this.Install();
                this.Precompile();

                if (!isPrefab)
                {
                    this.InitInEditMode();
                    this.EnableInEditMode();
                }

                Debug.Log($"<color=#00D4FF>{this.name} Compiled successfully!</color>", this);
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=#FF3C3C>{this.name} Compile failed: {e.Message}</color>\n{e.StackTrace}",
                    this);
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
    }
}

#endif
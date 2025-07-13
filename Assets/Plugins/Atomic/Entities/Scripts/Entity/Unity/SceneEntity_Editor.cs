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
    public partial class SceneEntity
    {
        private void Reset()
        {
            this.installers = new List<SceneEntityInstaller>(this.GetComponentsInChildren<SceneEntityInstaller>());
            this.children = new List<SceneEntity>(this.GetComponentsInChildren<SceneEntity>());
            this.children.Remove(this);
        }

        private void OnValidate()
        {
            this.AutoRefresh();
        }

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

        private void SetRefreshCallbackToInstallers()
        {
            foreach (SceneEntityInstaller installer in this.installers)
                if (installer != null)
                    installer.refreshCallback = this.RefreshInEditMode;
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Test Install"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        private void RefreshInEditMode()
        {
            this.ResetInstall();

            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;
            if (!isPrefab)
            {
                this.DisableInEditMode();
                this.DisposeInEditMode();
            }

            this.CreateEntity();
            this.Install();
            this.Precompile();

            if (!isPrefab)
            {
                _entity.Name = this.name;
                this.InitInEditMode();
                this.EnableInEditMode();
            }
        }

        private void InitInEditMode()
        {
            if (_entity.Initialized)
                return;

            foreach (IBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IInit dispose && IsEditModeSupported(behaviour))
                    dispose.Init(_entity);
        }

        private void EnableInEditMode()
        {
            if (_entity.Enabled)
                return;

            foreach (IBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IEnable dispose && IsEditModeSupported(behaviour))
                    dispose.Enable(_entity);
        }

        private void DisableInEditMode()
        {
            if (_entity is not {Enabled: true})
                return;

            foreach (IBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IDisable disable && IsEditModeSupported(behaviour))
                    disable.Disable(_entity);
        }

        private void DisposeInEditMode()
        {
            if (_entity is not {Initialized: true})
                return;

            foreach (IBehaviour behaviour in _entity.GetBehaviours())
            {
                if (behaviour is IDispose dispose && IsEditModeSupported(behaviour))
                    dispose.Dispose(_entity);
            }
        }

        private static bool IsEditModeSupported(IBehaviour behaviour)
        {
            return behaviour.GetType().IsDefined(typeof(EditModeBehaviourAttribute));
        }
    }
}

#endif
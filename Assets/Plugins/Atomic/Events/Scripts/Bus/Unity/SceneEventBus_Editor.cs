#if UNITY_EDITOR
using System;
using UnityEditor;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    public partial class SceneEventBus
    {
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
                this.RefreshInEditMode();
            }
            catch (Exception)
            {
                // ignored
            }
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
            _eventBus.Dispose();
            this.InstallInternal();
        }
    }
}

#endif
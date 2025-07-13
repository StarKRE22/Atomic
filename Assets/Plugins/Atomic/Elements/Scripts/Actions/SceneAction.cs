using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Scene Action")]
    public class SceneAction : SceneActionBase
    {
        [SerializeReference]
        private IAction[] actions = default;
        
        public SceneAction Compose(params IAction[] actions)
        {
            this.actions = actions;
            return this;
        }
        
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke()
        {
            if (this.actions == null)
            {
                return;
            }

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                IAction action = this.actions[i];
                action?.Invoke();
            }
        }
    }
}
using System;
using UnityEngine;
// ReSharper disable NotAccessedField.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public sealed class ActionGroup : IAction
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f), HideLabel]
#endif
        [SerializeField]
        private string name;
        
        [Space, SerializeReference]
        private IAction[] actions;

        public ActionGroup()
        {
        }

        public ActionGroup(params IAction[] actions)
        {
            this.actions = actions;
        }

        public void Compose(params IAction[] actions)
        {
            this.actions = actions;
        }

        public void Invoke()
        {
            this.actions.InvokeAll();
        }
    }
}
using System;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class SceneActionRef : IAction
    {
        [SerializeField]
        private SceneAction action;

        public SceneActionRef()
        {
        }

        public SceneActionRef(SceneAction action)
        {
            this.action = action;
        }

        public SceneActionRef Compose(SceneAction action)
        {
            this.action = action;
            return this;
        }

        public void Invoke()
        {
            if (this.action != null)
            {
                this.action.Invoke();
            }
        }
    }
}
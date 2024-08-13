using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif


namespace Atomic.Elements
{
    [Serializable]
    public sealed class DebugLogAction : IAction
    {
#if ODIN_INSPECTOR
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
#endif
        [SerializeField]
        private string message;
        
        public DebugLogAction(string message)
        {
            this.message = message;
        }

        public void Invoke()
        {
            Debug.Log(this.message);
        }
    }
}
using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    ///Provides setter interface to a specified source.

    [Serializable]
    public class Setter<T> : ISetter<T>
    {
        public T Value
        {
            set => this.action?.Invoke(value);
        }

        private System.Action<T> action;

        public Setter()
        {
        }

        public Setter(System.Action<T> action)
        {
            this.action = action;
        }

        public void Compose(System.Action<T> action)
        {
            this.action = action;
        }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("Set Value")]
#endif
        private void SetValueEditor(T value) => this.action?.Invoke(value);
#endif
    }
}

#if UNITY_5_3_OR_NEWER
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a group of <see cref="MonoAction"/> instances that can be invoked sequentially.
    /// Follows the Composite design pattern: the group itself behaves as a single scene action,
    /// while internally invoking all contained scene actions in order.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Action Composite")]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionComposite.md")]
    public class MonoActionComposite : MonoAction
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [Space, SerializeField]
        public MonoAction[] actions;

        /// <summary>
        /// Invokes all contained scene actions sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke()
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                MonoAction action = this.actions[i];
                if (action) action.Invoke();
            }
        }
    }

    /// <summary>
    /// Composite scene action with one generic parameter.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionComposite%601.md")]
    public class MonoActionComposite<T> : MonoAction<T>
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [Space, SerializeField]
        public MonoAction<T>[] actions;

        /// <summary>
        /// Invokes all contained scene actions sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T arg)
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                MonoAction<T> action = this.actions[i];
                if (action) action.Invoke(arg);
            }
        }
    }

    /// <summary>
    /// Composite scene action with two generic parameters.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionComposite%602.md")]
    public class MonoActionComposite<T1, T2> : MonoAction<T1, T2>
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [Space, SerializeField]
        public MonoAction<T1, T2>[] actions;

        /// <summary>
        /// Invokes all contained scene actions sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2)
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                MonoAction<T1, T2> action = this.actions[i];
                if (action) action.Invoke(arg1, arg2);
            }
        }
    }

    /// <summary>
    /// Composite scene action with three generic parameters.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionComposite%603.md")]
    public class MonoActionComposite<T1, T2, T3> : MonoAction<T1, T2, T3>
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [SerializeField]
        public MonoAction<T1, T2, T3>[] actions;

        /// <summary>
        /// Invokes all contained scene actions sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                MonoAction<T1, T2, T3> action = this.actions[i];
                if (action) action.Invoke(arg1, arg2, arg3);
            }
        }
    }

    /// <summary>
    /// Composite scene action with four generic parameters.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionComposite%604.md")]
    public class MonoActionComposite<T1, T2, T3, T4> : MonoAction<T1, T2, T3, T4>
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [SerializeField]
        public MonoAction<T1, T2, T3, T4>[] actions;

        /// <summary>
        /// Invokes all contained scene actions sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                MonoAction<T1, T2, T3, T4> action = this.actions[i];
                if (action) action.Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }
}
#endif
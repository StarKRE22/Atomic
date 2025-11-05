#if UNITY_5_3_OR_NEWER
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that executes a sequence of <see cref="IAction"/> instances
    /// when <see cref="Invoke"/> is called &mdash; either from code or through the Odin-generated button
    /// in the Inspector (when Odin Inspector is installed).
    /// </summary>
    /// <remarks>
    /// Attach this component to any GameObject that needs to trigger one-off or reusable logic
    /// without writing a custom script.  
    /// You can configure the action list in the Inspector or via <see cref="Construct"/>.
    /// </remarks>
    [AddComponentMenu("Atomic/Elements/Action")]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionDefault.md")]
    public class SceneActionDefault : SceneActionAbstract
    {
        /// <summary>
        /// Actions to run when this component is invoked.
        /// They are executed in the order they appear in the array.
        /// </summary>
        [SerializeReference]
        public IAction[] actions;

        /// <summary>
        /// Executes every action in <see cref="actions"/> sequentially.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke()
        {
            if (this.actions != null)
                for (int i = 0, count = this.actions.Length; i < count; i++)
                    this.actions[i]?.Invoke();
        }
    }

    /// <summary>
    /// Scene-based action with one parameter.
    /// Executes a sequence of <see cref="IAction{T1}"/> instances.
    /// </summary>
    /// <typeparam name="T">Type of the input parameter.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionDefault%601.md")]
    public abstract class SceneActionDefault<T> : SceneActionAbstract<T>
    {
        /// <summary>
        /// Actions to invoke sequentially. Can be assigned in the Inspector via [SerializeReference].
        /// </summary>
        [SerializeReference]
        public IAction<T>[] actions;

        /// <summary>
        /// Invokes all actions sequentially with the given argument.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="arg1">The input argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T arg1)
        {
            if (this.actions != null)
                for (int i = 0, count = this.actions.Length; i < count; i++)
                    this.actions[i]?.Invoke(arg1);
        }
    }

    /// <summary>
    /// Scene-based action with two parameters.
    /// Executes a sequence of <see cref="IAction{T1, T2}"/> instances.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <typeparam name="T2">Type of the second argument.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionDefault%602.md")]
    public abstract class SceneActionDefault<T1, T2> : SceneActionAbstract<T1, T2>
    {
        [SerializeReference]
        public IAction<T1, T2>[] actions;

        /// <summary>
        /// Invokes all actions sequentially with the given arguments.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2)
        {
            if (this.actions != null)
                for (int i = 0, count = this.actions.Length; i < count; i++)
                    this.actions[i]?.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Scene-based action with three parameters.
    /// Executes a sequence of <see cref="IAction{T1, T2, T3}"/> instances.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <typeparam name="T2">Type of the second argument.</typeparam>
    /// <typeparam name="T3">Type of the third argument.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionDefault%603.md")]
    public abstract class SceneActionDefault<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
    {
        [SerializeReference]
        public IAction<T1, T2, T3>[] actions;

        /// <summary>
        /// Invokes all actions sequentially with the given arguments.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (this.actions != null)
                for (int i = 0, count = this.actions.Length; i < count; i++)
                    this.actions[i]?.Invoke(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Scene-based action with four parameters.
    /// Executes a sequence of <see cref="IAction{T1, T2, T3, T4}"/> instances.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <typeparam name="T2">Type of the second argument.</typeparam>
    /// <typeparam name="T3">Type of the third argument.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionDefault%604.md")]
    public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
    {
        [SerializeReference]
        public IAction<T1, T2, T3, T4>[] actions;

        /// <summary>
        /// Invokes all actions sequentially with the given arguments.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (this.actions != null)
                for (int i = 0, count = this.actions.Length; i < count; i++)
                    this.actions[i]?.Invoke(arg1, arg2, arg3, arg4);
        }
    }
}
#endif
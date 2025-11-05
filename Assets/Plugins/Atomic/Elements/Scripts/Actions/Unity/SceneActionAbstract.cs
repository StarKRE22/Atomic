#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// An abstract class for scene-based actions that implement the <see cref="IAction"/> interface.
    /// Inherit from this class to define custom actions as MonoBehaviours.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionAbstract.md")]
    public abstract class SceneActionAbstract : MonoBehaviour, IAction
    {
        /// <inheritdoc cref="IAction.Invoke"/>
        public abstract void Invoke();
    }

    /// <summary>
    /// Scene-based action with one parameter.
    /// </summary>
    /// <typeparam name="T">Type of the input parameter.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionAbstract%601.md")]
    public abstract class SceneActionAbstract<T> : MonoBehaviour, IAction<T>
    {
        /// <inheritdoc cref="IAction{T}.Invoke"/>
        public abstract void Invoke(T arg);
    }

    /// <summary>
    /// Scene-based action with two parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionAbstract%602.md")]
    public abstract class SceneActionAbstract<T1, T2> : MonoBehaviour, IAction<T1, T2>
    {
        /// <inheritdoc cref="IAction{T1, T2}.Invoke"/>
        public abstract void Invoke(T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Scene-based action with three parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    /// <typeparam name="T3">Type of the third parameter.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionAbstract%603.md")]
    public abstract class SceneActionAbstract<T1, T2, T3> : MonoBehaviour, IAction<T1, T2, T3>
    {
        /// <inheritdoc cref="IAction{T1, T2, T3}.Invoke"/>
        public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3);
    }

    /// <summary>
    /// Scene-based action with four parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    /// <typeparam name="T3">Type of the third parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth parameter.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Actions/SceneActionAbstract%604.md")]
    public abstract class SceneActionAbstract<T1, T2, T3, T4> : MonoBehaviour, IAction<T1, T2, T3, T4>
    {
        /// <inheritdoc cref="IAction{T1, T2, T3, T4}.Invoke"/>
        public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
}
#endif
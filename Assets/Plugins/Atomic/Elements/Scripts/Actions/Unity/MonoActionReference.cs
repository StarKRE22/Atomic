#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A pointer wrapper for invoking a <see cref="MonoAction"/> from outside the scene context.
    /// </summary>
    /// <remarks>
    /// This class allows referencing and triggering a <see cref="MonoAction"/> as a serializable field,
    /// often useful in serialized action chains or as part of a logic asset.
    /// </remarks>
    [Serializable]
    public sealed class MonoActionReference : IAction
    {
        /// <summary>
        /// Reference to the actual <see cref="MonoAction"/> component to be invoked.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        public MonoAction action;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector**.
        /// </remarks>
        public MonoActionReference()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MonoActionReference"/> with the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="MonoAction"/> to wrap.</param>
        public MonoActionReference(MonoAction action) => this.action = action;

        /// <summary>
        /// Invokes the referenced <see cref="MonoAction"/>, if it exists.
        /// </summary>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public void Invoke()
        {
            if (this.action)
                this.action.Invoke();
        }
    }

    /// <summary>
    /// Reference wrapper for a <see cref="MonoAction{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the argument for the scene action.</typeparam>
    [Serializable]
    public sealed class MonoActionReference<T> : IAction<T>
    {
        /// <summary>
        /// The actual scene action to invoke.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        public MonoAction<T> action;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector**.
        /// </remarks>
        public MonoActionReference()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MonoActionReference{T}"/> wrapping the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="MonoAction{T}"/> to reference.</param>
        public MonoActionReference(MonoAction<T> action) => this.action = action;

        /// <summary>
        /// Invokes the referenced scene action with the given argument.
        /// </summary>
        /// <param name="arg">The argument to pass to the action.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public void Invoke(T arg)
        {
            if (this.action)
                this.action.Invoke(arg);
        }
    }

    /// <summary>
    /// Reference wrapper for a <see cref="MonoAction{T1,T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    [Serializable]
    public sealed class MonoActionReference<T1, T2> : IAction<T1, T2>
    {
        /// <summary>
        /// The actual scene action to invoke.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        public MonoAction<T1, T2> action;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector**.
        /// </remarks>
        public MonoActionReference()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MonoActionReference{T1,T2}"/> wrapping the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="MonoAction{T1,T2}"/> to reference.</param>
        public MonoActionReference(MonoAction<T1, T2> action) => this.action = action;

        /// <summary>
        /// Invokes the referenced scene action with the given arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2)
        {
            if (this.action)
                this.action.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Reference wrapper for a <see cref="MonoAction{T1,T2,T3}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    [Serializable]
    public sealed class MonoActionReference<T1, T2, T3> : IAction<T1, T2, T3>
    {
        /// <summary>
        /// The actual scene action to invoke.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        public MonoAction<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector**.
        /// </remarks>
        public MonoActionReference()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MonoActionReference{T1,T2,T3}"/> wrapping the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="MonoAction{T1,T2,T3}"/> to reference.</param>
        public MonoActionReference(MonoAction<T1, T2, T3> action) => this.action = action;

        /// <summary>
        /// Invokes the referenced scene action with the given arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (this.action)
                this.action.Invoke(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Reference wrapper for a <see cref="MonoAction{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    [Serializable]
    public sealed class MonoActionReference<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
    {
        /// <summary>
        /// The actual scene action to invoke.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        public MonoAction<T1, T2, T3, T4> action;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector**.
        /// </remarks>
        public MonoActionReference()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MonoActionReference{T1,T2,T3,T4}"/> wrapping the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="MonoAction{T1,T2,T3,T4}"/> to reference.</param>
        public MonoActionReference(MonoAction<T1, T2, T3, T4> action) => this.action = action;

        /// <summary>
        /// Invokes the referenced scene action with the given arguments.
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
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (this.action)
                this.action.Invoke(arg1, arg2, arg3, arg4);
        }
    }
}
#endif
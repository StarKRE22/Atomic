using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a base implementation of a request without parameters.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class Request : IRequest
    {
        /// <inheritdoc />
        public bool Required => _required;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool _required;

        /// <summary>
        /// Marks the request as required.
        /// </summary>
        public void Invoke() => _required = true;

        /// <inheritdoc />
        public bool Consume()
        {
            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    /// <summary>
    /// Represents a base implementation of a request with a single argument.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    [Serializable]
    public class Request<T> : IRequest<T>
    {
        /// <inheritdoc />
        public bool Required => _required;

        /// <inheritdoc />
        public T Arg => _arg;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T _arg;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool _required;

        /// <summary>
        /// Marks the request as required and assigns an argument.
        /// </summary>
        /// <param name="arg">The argument to be stored.</param>
        public void Invoke(T arg)
        {
            _required = true;
            _arg = arg;
        }

        /// <inheritdoc />
        public bool TryGet(out T arg)
        {
            arg = _arg;
            return _required;
        }

        /// <inheritdoc />
        public bool Consume(out T arg)
        {
            arg = _arg;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    /// <summary>
    /// Represents a base implementation of a request with two arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    [Serializable]
    public class Request<T1, T2> : IRequest<T1, T2>
    {
        /// <inheritdoc />
        public bool Required => _required;

        /// <inheritdoc />
        public T1 Arg1 => _arg1;

        /// <inheritdoc />
        public T2 Arg2 => _arg2;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T1 _arg1;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T2 _arg2;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool _required;

        /// <summary>
        /// Marks the request as required and assigns both arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        public void Invoke(T1 arg1, T2 arg2)
        {
            _required = true;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        /// <inheritdoc />
        public bool TryGet(out T1 arg1, out T2 arg2)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            return _required;
        }

        /// <inheritdoc />
        public bool Consume(out T1 arg1, out T2 arg2)
        {
            arg1 = _arg1;
            arg2 = _arg2;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    /// <summary>
    /// Represents a base implementation of a request with three arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    [Serializable]
    public class Request<T1, T2, T3> : IRequest<T1, T2, T3>
    {
        /// <inheritdoc />
        public bool Required => _required;

        /// <inheritdoc />
        public T1 Arg1 => _arg1;

        /// <inheritdoc />
        public T2 Arg2 => _arg2;

        /// <inheritdoc />
        public T3 Arg3 => _arg3;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T1 _arg1;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T2 _arg2;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T3 _arg3;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool _required;

        /// <summary>
        /// Marks the request as required and assigns all three arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            _required = true;
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
        }

        /// <inheritdoc />
        public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;
            return _required;
        }

        /// <inheritdoc />
        public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    /// <summary>
    /// Represents a base implementation of a request with four arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    [Serializable]
    public class Request<T1, T2, T3, T4> : IRequest<T1, T2, T3, T4>
    {
        /// <inheritdoc />
        public bool Required => _required;

        /// <inheritdoc />
        public T1 Arg1 => _arg1;

        /// <inheritdoc />
        public T2 Arg2 => _arg2;

        /// <inheritdoc />
        public T3 Arg3 => _arg3;

        /// <inheritdoc />
        public T4 Arg4 => _arg4;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T1 _arg1;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T2 _arg2;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T3 _arg3;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T4 _arg4;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool _required;

        /// <summary>
        /// Marks the request as required and assigns all four arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _required = true;
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
        }

        /// <inheritdoc />
        public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;
            arg4 = _arg4;
            return _required;
        }

        /// <inheritdoc />
        public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;
            arg4 = _arg4;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }
}
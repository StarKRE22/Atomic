using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a single member of an expression with no input parameters.
    /// Encapsulates a delegate that produces a value of type <typeparamref name="R"/>
    /// and optionally stores the object that owns or registered the delegate.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public struct ExpressionMember<R>
    {
        /// <summary>
        /// Gets the object associated with this expression member.
        /// Typically used to identify the owner or registration source.
        /// Returns <see langword="null"/> if no source was provided.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;
        
        private object _source;
        private Func<R> _func;

        /// <summary>
        /// Initializes the member from a delegate.
        /// </summary>
        /// <param name="func">The delegate that produces the result.</param>
        public ExpressionMember(Func<R> func)
        {
            _source = null;
            _func = func;
        }

        /// <summary>
        /// Initializes the member from an <see cref="IFunction{R}"/>.
        /// </summary>
        /// <param name="func">The function wrapper to invoke.</param>
        public ExpressionMember(IFunction<R> func)
        {
            _source = null;
            _func = func.Invoke;
        }

        /// <summary>
        /// Initializes the member with an associated source object and delegate.
        /// </summary>
        /// <param name="source">The object associated with the delegate.</param>
        /// <param name="func">The delegate that produces the result.</param>
        public ExpressionMember(object source, Func<R> func)
        {
            _source = source;
            _func = func;
        }

        /// <summary>
        /// Initializes the member with an associated source object and function wrapper.
        /// </summary>
        /// <param name="source">The object associated with the function.</param>
        /// <param name="func">The function wrapper to invoke.</param>
        public ExpressionMember(object source, IFunction<R> func)
        {
            _source = source;
            _func = func.Invoke;
        }

        /// <summary>
        /// Determines whether this member wraps the specified delegate instance.
        /// </summary>
        /// <param name="func">The delegate to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the wrapped delegate matches the specified delegate;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public readonly bool EqualsFunction(Func<R> func)
        {
            return _func == func;
        }

        /// <summary>
        /// Invokes the wrapped delegate.
        /// </summary>
        /// <returns>The value returned by the delegate.</returns>
        public readonly R Invoke()
        {
            return _func.Invoke();
        }
    }

    /// <summary>
    /// Represents a single member of an expression with one input parameter.
    /// Encapsulates a delegate that maps a value of type <typeparamref name="T"/>
    /// to a result of type <typeparamref name="R"/>, and optionally stores the
    /// object that owns or registered the delegate.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
    [Serializable]
    public struct ExpressionMember<T, R>
    {
        /// <summary>
        /// Gets the object associated with this expression member.
        /// Typically used to identify the owner or registration source.
        /// Returns <see langword="null"/> if no source was provided.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;

        private object _source;
        private Func<T, R> _func;

        /// <summary>
        /// Initializes the member from a delegate.
        /// </summary>
        /// <param name="func">The delegate to wrap.</param>
        public ExpressionMember(Func<T, R> func)
        {
            _source = null;
            _func = func;
        }

        /// <summary>
        /// Initializes the member with an associated source object and delegate.
        /// </summary>
        /// <param name="source">The object associated with the delegate.</param>
        /// <param name="func">The delegate to wrap.</param>
        public ExpressionMember(object source, Func<T, R> func)
        {
            _source = source;
            _func = func;
        }

        /// <summary>
        /// Determines whether this member wraps the specified delegate instance.
        /// </summary>
        /// <param name="func">The delegate to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the wrapped delegate matches the specified delegate;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public readonly bool EqualsFunction(Func<T, R> func)
        {
            return _func == func;
        }

        /// <summary>
        /// Invokes the wrapped delegate with the specified argument.
        /// </summary>
        /// <param name="arg">The input argument.</param>
        /// <returns>The value returned by the delegate.</returns>
        public readonly R Invoke(T arg)
        {
            return _func.Invoke(arg);
        }
    }

    /// <summary>
    /// Represents a single member of an expression with two input parameters.
    /// Encapsulates a delegate that maps values of types
    /// <typeparamref name="T1"/> and <typeparamref name="T2"/>
    /// to a result of type <typeparamref name="R"/>, and optionally stores the
    /// object that owns or registered the delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
    [Serializable]
    public struct ExpressionMember<T1, T2, R>
    {
        /// <summary>
        /// Gets the object associated with this expression member.
        /// Typically used to identify the owner or registration source.
        /// Returns <see langword="null"/> if no source was provided.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;

        private object _source;
        private Func<T1, T2, R> _func;

        /// <summary>
        /// Initializes the member from a delegate.
        /// </summary>
        /// <param name="func">The delegate to wrap.</param>
        public ExpressionMember(Func<T1, T2, R> func)
        {
            _source = null;
            _func = func;
        }

        /// <summary>
        /// Initializes the member with an associated source object and delegate.
        /// </summary>
        /// <param name="source">The object associated with the delegate.</param>
        /// <param name="func">The delegate to wrap.</param>
        public ExpressionMember(object source, Func<T1, T2, R> func)
        {
            _source = source;
            _func = func;
        }

        /// <summary>
        /// Determines whether this member wraps the specified delegate instance.
        /// </summary>
        /// <param name="func">The delegate to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the wrapped delegate matches the specified delegate;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public readonly bool EqualsFunction(Func<T1, T2, R> func)
        {
            return _func == func;
        }

        /// <summary>
        /// Invokes the wrapped delegate with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first input argument.</param>
        /// <param name="arg2">The second input argument.</param>
        /// <returns>The value returned by the delegate.</returns>
        public readonly R Invoke(T1 arg1, T2 arg2)
        {
            return _func.Invoke(arg1, arg2);
        }
    }
}

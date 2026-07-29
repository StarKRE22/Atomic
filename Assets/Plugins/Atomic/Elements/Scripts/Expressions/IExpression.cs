using System;
using System.Collections.Generic;

// ReSharper disable  PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression that produces a value of type <typeparamref name="R"/>.
    /// Provides list semantics for storing delegates of type <see cref="Func{R}"/>.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    public interface IExpression<R> :
        IReactiveList<ExpressionMember<R>>,
        IValue<R>
    {
    }

    /// <summary>
    /// Represents an expression that maps an input of type <typeparamref name="T"/> 
    /// to a result of type <typeparamref name="R"/>.
    /// Provides list semantics for storing delegates of type <see cref="Func{T,R}"/>.
    /// </summary>
    /// <typeparam name="T">The input type of the expression.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    public interface IExpression<T, R> : 
        IReactiveList<ExpressionMember<T, R>>, 
        IFunction<T, R>
    {
    }

    /// <summary>
    /// Represents an expression that maps two inputs of types <typeparamref name="T1"/> and <typeparamref name="T2"/> 
    /// to a result of type <typeparamref name="R"/>.
    /// Provides list semantics for storing delegates of type <see cref="Func{T1,T2,R}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first input argument.</typeparam>
    /// <typeparam name="T2">The type of the second input argument.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    public interface IExpression<T1, T2, R> :
        IReactiveList<ExpressionMember<T1, T2, R>>, 
        IFunction<T1, T2, R>
    {
    }
}
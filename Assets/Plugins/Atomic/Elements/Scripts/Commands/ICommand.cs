using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an executable command that can be conditionally invoked
    /// and notifies subscribers when it is executed.
    /// </summary>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction"/> and <see cref="ISignal"/>.
    /// A command can have execution conditions that determine whether it can be
    /// invoked, and supports dynamically adding or removing actions and conditions.
    /// </remarks>
    public interface ICommand : IAction, ISignal
    {
        /// <summary>
        /// Determines whether the command can be invoked.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all execution conditions are satisfied;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        bool CanInvoke();
    
        /// <summary>
        /// Attempts to invoke the command.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the command was successfully invoked;
        /// otherwise, <see langword="false"/> if execution conditions were not met.
        /// </returns>
        bool TryInvoke();
    
        /// <summary>
        /// Adds an execution condition to the command.
        /// The command can be invoked only if all registered conditions evaluate to
        /// <see langword="true"/>.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand AddCondition(Func<bool> condition);
    
        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand RemoveCondition(Func<bool> condition);
    
        /// <summary>
        /// Adds an action to be executed when the command is invoked.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand AddAction(Action action);
    
        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand RemoveAction(Action action);
    }
    
    /// <summary>
    /// Represents an executable command that takes one argument, can be conditionally
    /// invoked, and notifies subscribers when it is executed.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T}"/> and <see cref="ISignal{T}"/>.
    /// A command can have execution conditions that determine whether it can be
    /// invoked, and supports dynamically adding or removing actions and conditions.
    /// </remarks>
    public interface ICommand<T> : IAction<T>, ISignal<T>
    {
        /// <summary>
        /// Determines whether the command can be invoked with the specified argument.
        /// </summary>
        /// <param name="arg">The input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command can be invoked; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool CanInvoke(T arg);
    
        /// <summary>
        /// Attempts to invoke the command with the specified argument.
        /// </summary>
        /// <param name="arg">The input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command was successfully invoked;
        /// otherwise, <see langword="false"/> if execution conditions were not met.
        /// </returns>
        bool TryInvoke(T arg);
    
        /// <summary>
        /// Adds an execution condition to the command.
        /// The command can be invoked only if all registered conditions evaluate to
        /// <see langword="true"/> for the specified argument.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T> AddCondition(Func<T, bool> condition);
    
        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T> RemoveCondition(Func<T, bool> condition);
    
        /// <summary>
        /// Adds an action to be executed when the command is invoked.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T> AddAction(Action<T> action);
    
        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T> RemoveAction(Action<T> action);
    }

    /// <summary>
    /// Represents an executable command that takes two arguments, can be conditionally
    /// invoked, and notifies subscribers when it is executed.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T1, T2}"/> and
    /// <see cref="ISignal{T1, T2}"/>. A command can have execution conditions that
    /// determine whether it can be invoked, and supports dynamically adding or
    /// removing actions and conditions.
    /// </remarks>
    public interface ICommand<T1, T2> : IAction<T1, T2>, ISignal<T1, T2>
    {
        /// <summary>
        /// Determines whether the command can be invoked with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first input parameter.</param>
        /// <param name="arg2">The second input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command can be invoked; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool CanInvoke(T1 arg1, T2 arg2);
    
        /// <summary>
        /// Attempts to invoke the command with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first input parameter.</param>
        /// <param name="arg2">The second input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command was successfully invoked;
        /// otherwise, <see langword="false"/> if execution conditions were not met.
        /// </returns>
        bool TryInvoke(T1 arg1, T2 arg2);
    
        /// <summary>
        /// Adds an execution condition to the command.
        /// The command can be invoked only if all registered conditions evaluate to
        /// <see langword="true"/> for the specified arguments.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition);
    
        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition);
    
        /// <summary>
        /// Adds an action to be executed when the command is invoked.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2> AddAction(Action<T1, T2> action);
    
        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2> RemoveAction(Action<T1, T2> action);
    }
    
    /// <summary>
    /// Represents an executable command that takes four arguments, can be conditionally
    /// invoked, and notifies subscribers when it is executed.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="T3">The type of the third input parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth input parameter.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T1, T2, T3, T4}"/> and
    /// <see cref="ISignal{T1, T2, T3, T4}"/>. A command can have execution
    /// conditions that determine whether it can be invoked, and supports
    /// dynamically adding or removing actions and conditions.
    /// </remarks>
    public interface ICommand<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, ISignal<T1, T2, T3, T4>
    {
        /// <summary>
        /// Determines whether the command can be invoked with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first input parameter.</param>
        /// <param name="arg2">The second input parameter.</param>
        /// <param name="arg3">The third input parameter.</param>
        /// <param name="arg4">The fourth input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command can be invoked; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    
        /// <summary>
        /// Attempts to invoke the command with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first input parameter.</param>
        /// <param name="arg2">The second input parameter.</param>
        /// <param name="arg3">The third input parameter.</param>
        /// <param name="arg4">The fourth input parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the command was successfully invoked;
        /// otherwise, <see langword="false"/> if execution conditions were not met.
        /// </returns>
        bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    
        /// <summary>
        /// Adds an execution condition to the command.
        /// The command can be invoked only if all registered conditions evaluate to
        /// <see langword="true"/> for the specified arguments.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition);
    
        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition);
    
        /// <summary>
        /// Adds an action to be executed when the command is invoked.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action);
    
        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        /// <returns>The current command instance.</returns>
        ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action);
    }
}

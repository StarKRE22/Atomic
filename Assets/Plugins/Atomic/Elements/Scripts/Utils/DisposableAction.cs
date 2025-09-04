using System;

namespace Atomic.Elements
{
    /// <summary>
    /// A simple <see cref="IDisposable"/> implementation that invokes a specified action when disposed.
    /// Useful for inline or ad-hoc cleanup logic.
    /// </summary>
    public readonly struct DisposableAction : IDisposable
    {
        /// <summary>
        /// The action to invoke when the object is disposed.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableAction"/> class.
        /// </summary>
        /// <param name="action">The action to invoke when <see cref="Dispose"/> is called.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
        public DisposableAction(Action action) => _action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Invokes the action specified at construction time.
        /// </summary>
        public void Dispose() => _action.Invoke();
    }
}
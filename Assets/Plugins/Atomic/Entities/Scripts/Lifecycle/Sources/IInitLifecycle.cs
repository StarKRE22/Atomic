using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a contract that can be explicitly initialized and disposed during its lifecycle.
    /// </summary>
    public interface IInitLifecycle : IDisposable
    {
        /// <summary>
        /// Occurs when the object has been successfully initialized.
        /// </summary>
        event Action OnInitialized;

        /// <summary>
        /// Occurs when the object has been disposed and its resources released.
        /// </summary>
        event Action OnDisposed;

        /// <summary>
        /// Gets a value indicating whether the object is currently initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Initializes the object, transitioning it into the initialized state.
        /// </summary>
        void Init();
    }
}
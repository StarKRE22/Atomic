using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a contract that can be enabled or disabled during runtime.
    /// </summary>
    public interface IEnableLifecycle
    {
        /// <summary>
        /// Occurs when the object is enabled.
        /// </summary>
        event Action OnEnabled;

        /// <summary>
        /// Occurs when the object is disabled.
        /// </summary>
        event Action OnDisabled;

        /// <summary>
        /// Gets a value indicating whether the object is currently enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Enables the object.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the object.
        /// </summary>
        void Disable();
    }
}
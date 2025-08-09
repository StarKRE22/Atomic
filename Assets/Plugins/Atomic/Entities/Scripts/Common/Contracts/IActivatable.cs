using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents an object that can be enabled or disabled during runtime.
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Occurs when the object is enabled.
        /// </summary>
        event Action OnActivated;

        /// <summary>
        /// Occurs when the object is disabled.
        /// </summary>
        event Action OnDeactivated;

        /// <summary>
        /// Gets a value indicating whether the object is currently enabled.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Enables the object.
        /// </summary>
        void Activate();

        /// <summary>
        /// Disables the object.
        /// </summary>
        void Deactivate();
    }
}
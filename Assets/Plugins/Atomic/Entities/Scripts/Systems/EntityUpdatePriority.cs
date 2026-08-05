namespace Atomic.Entities
{
    /// <summary>
    /// Defines the relative update priority of an entity system.
    /// Systems with higher priority are typically updated before lower-priority systems.
    /// </summary>
    public enum EntityUpdatePriority : byte
    {
        /// <summary>
        /// Lowest update priority.
        /// </summary>
        Low = 0,

        /// <summary>
        /// Default update priority.
        /// </summary>
        Medium = 1,

        /// <summary>
        /// Highest update priority.
        /// </summary>
        High = 2
    }
}

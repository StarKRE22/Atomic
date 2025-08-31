namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that can be updated over time via ticks.
    /// </summary>
    public interface ITickSource
    {
        /// <summary>
        /// Updates the source by delta time.
        /// </summary>
        /// <param name="deltaTime">The time increment.</param>
        void Tick(float deltaTime);
    }
}
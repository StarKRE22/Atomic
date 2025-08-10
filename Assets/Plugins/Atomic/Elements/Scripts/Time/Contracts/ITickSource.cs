namespace Atomic.Elements
{
    public interface ITickSource
    {
        /// <summary>
        /// Updates the cooldown by reducing current time by deltaTime.
        /// Fires <see cref="Cooldown.OnExpired.OnExpired"/> if the timer reaches zero.
        /// </summary>
        /// <param name="deltaTime">The time to subtract from the current time.</param>
        void Tick(float deltaTime);
    }
}
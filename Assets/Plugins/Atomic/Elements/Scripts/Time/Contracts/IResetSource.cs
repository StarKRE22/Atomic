namespace Atomic.Elements
{
    public interface IResetSource
    {
        /// <summary>
        /// Resets the cooldown to full duration.
        /// </summary>
        void Reset();
    }
}
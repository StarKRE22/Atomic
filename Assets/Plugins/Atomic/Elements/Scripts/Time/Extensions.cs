namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for <see cref="IStartSource"/> to simplify restarting timers or countdowns.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Stops the source and restarts it from a specific time.
        /// </summary>
        /// <param name="source">The source to restart.</param>
        /// <param name="time">The time to start from.</param>
        public static void Restart(this IStartSource source, float time)
        {
            source.Stop();
            source.Start(time);
        }
        
        /// <summary>
        /// Stops the source and restarts it from the default start time.
        /// </summary>
        /// <param name="source">The source to restart.</param>
        public static void Restart(this IStartSource source)
        {
            source.Stop();
            source.Start();
        }
    }
}
using System;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Restart(this IStartSource source, float time)
        {
            source.Stop();
            source.Start(time);
        }
        
        /// <summary>
        /// Stops the source and restarts it from the default start time.
        /// </summary>
        /// <param name="source">The source to restart.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Restart(this IStartSource source)
        {
            source.Stop();
            source.Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObserveTimeChanged(this ITimeSource source, Action<float> action)
        {
            action.Invoke(source.GetTime());
            source.OnTimeChanged += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotCompleted(this ICompleteSource source) => !source.IsCompleted();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIdleOrExpired(this ITimestamp timestamp) => timestamp.IsIdle() || timestamp.IsExpired();
    }
}
namespace Atomic.Elements
{
    public static partial class Extensions
    {
        public static DurationTimestamp WithDuration(this ITimestamp timestamp, float durationSeconds) => 
            new(timestamp, durationSeconds);
    }
}
namespace Atomic.Elements
{
    public static partial class Extensions
    {
        public static void Restart(this IStartSource source, float currentTime = 0)
        {
            source.Stop();
            source.Start(currentTime);
        }
    }
}
namespace Atomic.Elements.Plugins.Atomic.Elements.Scripts.Time
{
    public static class Extensions
    {
        public static void Restart(this IStartSource source, float currentTime = 0)
        {
            source.Stop();
            source.Start(currentTime);
        }
    }
}
namespace Atomic.Elements
{
    public static partial class Extensions
    {
        public static void Restart(this IStartSource source, float time)
        {
            source.Stop();
            source.Start(time);
        }
        
        public static void Restart(this IStartSource source)
        {
            source.Stop();
            source.Start();
        }
    }
}
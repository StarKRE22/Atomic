namespace Atomic.Elements.Plugins.Atomic.Elements.Scripts.Time
{
    public static class Extensions
    {
        public void Restart(float currentTime = 0)
        {
            this.Stop();
            this.Start(currentTime);
        }
    }
}
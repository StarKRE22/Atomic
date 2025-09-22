namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        private const string SAMPLE = nameof(SAMPLE);
        private static readonly object Dummy = new();
        private const int N = 1000;
    }
}
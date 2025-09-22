using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void HasValue()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        _ = entity.HasValue(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
    }
}
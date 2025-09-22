using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void ClearValues()
        {
            var entity = new Entity();

            Measure
                .Method(() => entity.ClearValues())
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ClearValues (full)", SampleUnit.Microsecond))
                .Run();
        }
    }
}
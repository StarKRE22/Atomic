using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void SetValue_AsObject()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, Dummy);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void SetValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, 777);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void SetValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, SAMPLE);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
    }
}
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void AddValue_AsObject()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.AddValue(i, Dummy);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue (object-ref)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void AddValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.AddValue(i, 777);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue (object-int)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void AddValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.AddValue(i, SAMPLE);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue<T> (string)", SampleUnit.Microsecond))
                .Run();
        }
    }
}
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void TryGetValue_Object()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, Dummy);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValue(i, out object unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue (object)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValue(i, out string unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue<T> (string)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValue(i, out int unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue<T> (int)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValueUnsafe_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValueUnsafe(i, out string unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValueUnsafe<T> (string)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValueUnsafe_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValueUnsafe(i, out int unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValueUnsafe<T> (int)", SampleUnit.Microsecond))
                .Run();
        }
    }
}
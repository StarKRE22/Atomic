#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class Entity_GetValue_Performance
    {
        [Test, Performance]
        public void GetValue_Entity_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < 1000; i++)
                entity.AddValue(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string unused = entity.GetValue<string>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void GetValue_Entity_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < 1000; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        int unused = entity.GetValue<int>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void GetValue_Entity_AsReferenceUnsafe()
        {
            var entity = new Entity();
            for (int i = 0; i < 1000; i++)
                entity.AddValue(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string unused = entity.GetValueUnsafe<string>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void GetValue_Entity_AsPrimitiveUnsafe()
        {
            var entity = new Entity();
            for (int i = 0; i < 1000; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        int unused = entity.GetValueUnsafe<int>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
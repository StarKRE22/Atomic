#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void GetValue_AsObject()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, Dummy);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        object unused = entity.GetValue(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        
        [Test, Performance]
        public void GetValue_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
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
        public void GetValue_Primitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
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
        public void GetValueUnsafe_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
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
        public void GetValueUnsafe_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
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
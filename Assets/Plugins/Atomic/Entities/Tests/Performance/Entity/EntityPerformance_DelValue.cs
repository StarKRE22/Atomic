using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void DelValue_AsObject()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void DelValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, 777);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }
        
           
        [Test, Performance]
        public void DelValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, SAMPLE);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }
    }
}
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void AddTag()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++) 
                        entity.AddTag(i);
                })
                .CleanUp(entity.ClearTags)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddTag", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void HasTag_Performance()
        {
            var entity = new Entity();

            for (int i = 0; i < N; i++)
                entity.AddTag(i);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool exists = entity.HasTag(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("HasTag", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void DelTag()
        {
            var entity = new Entity();
            
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++) 
                        entity.DelTag(i);
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddTag(i);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelTag", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ClearTags()
        {
            var entity = new Entity();

            Measure.Method(entity.ClearTags)
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddTag(i);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ClearTags", SampleUnit.Microsecond))
                .Run();
        }
    }
}
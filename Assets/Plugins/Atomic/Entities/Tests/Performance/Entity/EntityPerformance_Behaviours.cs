using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        [Test, Performance]
        public void AddBehaviour()
        {
            var behaviours = new IEntityBehaviour[N];
            for (int i = 0; i < N; i++)
                behaviours[i] = new EntityBehaviourDummy();

            var entity = new Entity();


            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddBehaviour(behaviours[i]);
                })
                .CleanUp(entity.ClearBehaviours)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddBehaviour", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void DelBehaviour()
        {
            var behaviours = new IEntityBehaviour[N];
            for (int i = 0; i < N; i++)
                behaviours[i] = new EntityBehaviourDummy();

            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.DelBehaviour(behaviours[i]);
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddBehaviour(behaviours[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("DelBehaviour", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ClearBehaviours()
        {
            var behaviours = new IEntityBehaviour[N];
            for (int i = 0; i < N; i++)
                behaviours[i] = new EntityBehaviourDummy();

            var entity = new Entity();

            Measure.Method(entity.ClearBehaviours)
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddBehaviour(behaviours[i]);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ClearBehaviours", SampleUnit.Microsecond))
                .Run();
        }
    }
}
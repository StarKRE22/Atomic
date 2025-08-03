using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public sealed class EntityCollection_Enumerator_Performance
    {
        [Test, Performance]
        public void Enumerator_EntityCollection()
        {
            var collection = new EntityCollection<Entity>();
            for (int i = 0; i < 1000; i++) 
                collection.Add(new Entity());

            Measure.Method(() =>
                {
                    foreach (Entity entity in collection) 
                        _ = entity;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator_List()
        {
            var list = new List<Entity>();
            for (int i = 0; i < 1000; i++) 
                list.Add(new Entity());

            Measure.Method(() =>
                {
                    foreach (var entity in list) 
                        _ = entity;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator_HashSet()
        {
            var hashSet = new HashSet<Entity>();
            for (int i = 0; i < 1000; i++) 
                hashSet.Add(new Entity());

            Measure.Method(() =>
                {
                    foreach (var entity in hashSet) 
                        _ = entity;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Enumerator", SampleUnit.Microsecond))
                .Run();
        }
    }
}
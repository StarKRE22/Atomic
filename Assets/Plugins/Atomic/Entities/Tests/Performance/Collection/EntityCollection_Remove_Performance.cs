#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public sealed class EntityCollection_Remove_Performance
    {
        [Test, Performance]
        public void Remove_EntityCollection()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var collection = new EntityCollection<Entity>();
            Measure.Method(() =>
                {
                    foreach (var entity in entities)
                        collection.Remove(entity);
                })
                .SetUp(() => collection.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Remove()"))
                .Run();
        }

        [Test, Performance]
        public void Remove_List()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var list = new List<Entity>();
            Measure.Method(() =>
                {
                    foreach (var entity in entities)
                        list.Remove(entity);
                })
                .SetUp(() => list.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Remove()"))
                .Run();
        }

        [Test, Performance]
        public void Remove_HashSet()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());
            
            var hashSet = new HashSet<Entity>();
            Measure.Method(() =>
                {
                    foreach (var entity in entities)
                        hashSet.Remove(entity);
                })
                .SetUp(() => hashSet.UnionWith(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Remove()"))
                .Run();
        }
    }
}
#endif
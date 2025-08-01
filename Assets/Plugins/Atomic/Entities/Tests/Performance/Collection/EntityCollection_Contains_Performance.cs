#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace Atomic.Entities
{
    public sealed class EntityCollection_Contains_Performance
    {
        [Test, Performance]
        public void Contains_EntityCollection()
        {
            var collection = new EntityCollection<Entity>();
            var entities = new List<Entity>();
        
            for (int i = 0; i < 1000; i++)
            {
                var entity = new Entity();
                collection.Add(entity);
                entities.Add(entity);
            }
        
            Measure.Method(() =>
                {
                    for (int i = 0; i < entities.Count; i++)
                        collection.Contains(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Contains()"))
                .Run();
        }

        [Test, Performance]
        public void Contains_List()
        {
            var list = new List<Entity>();
            var entities = new List<Entity>();

            for (int i = 0; i < 1000; i++)
            {
                var entity = new Entity();
                list.Add(entity);
                entities.Add(entity);
            }

            Measure.Method(() =>
                {
                    for (int i = 0; i < entities.Count; i++)
                        list.Contains(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Contains()"))
                .Run();
        }

        [Test, Performance]
        public void Contains_HashSet()
        {
            var hashSet = new HashSet<Entity>();
            var entities = new List<Entity>();

            for (int i = 0; i < 1000; i++)
            {
                var entity = new Entity();
                hashSet.Add(entity);
                entities.Add(entity);
            }

            Measure.Method(() =>
                {
                    for (int i = 0; i < entities.Count; i++)
                        hashSet.Contains(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Contains()"))
                .Run();
        }
    }
}
#endif
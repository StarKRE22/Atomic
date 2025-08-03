#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public sealed class EntityCollection_Performance
    {
        [Test, Performance]
        public void Add_EntityCollection()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            Measure.Method(() =>
                {
                    var collection = new EntityCollection<Entity>();
                    foreach (var t in entities)
                        collection.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Add()"))
                .Run();
        }


        [Test, Performance]
        public void Contains()
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
        public void Remove()
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
        public void Clear()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var collection = new EntityCollection<Entity>();
            Measure
                .Method(collection.Clear)
                .SetUp(() => collection.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Clear()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
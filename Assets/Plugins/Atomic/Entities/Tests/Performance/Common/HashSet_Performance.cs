using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public sealed class HashSet_Performance
    {
        [Test, Performance]
        public void Contains()
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
        
        [Test, Performance]
        public void Add()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            Measure.Method(() =>
                {
                    var set = new HashSet<Entity>();
                    foreach (var t in entities)
                        set.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Add()"))
                .Run();
        }
        
        [Test, Performance]
        public void Clear()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var hashSet = new HashSet<Entity>();
            Measure
                .Method(hashSet.Clear)
                .SetUp(() => hashSet.UnionWith(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Clear()", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void Enumerator()
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
        
        [Test, Performance]
        public void Remove()
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
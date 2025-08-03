using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class List_Performance
    {
        [Test, Performance]
        public void Add()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            Measure.Method(() =>
                {
                    var list = new List<Entity>();
                    foreach (var t in entities)
                        list.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Add()"))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var list = new List<Entity>();
            Measure
                .Method(list.Clear)
                .SetUp(() => list.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Clear()", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void Contains()
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
        public void Enumerator()
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
        public void Remove()
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
    }
}
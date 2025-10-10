#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace Atomic.Entities
{
    public sealed class EntityCollectionPerformance
    {
        private const int N = 1000;
        private Entity[] _source;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new Entity[N];
            for (int i = 0; i < N; i++)
                _source[i] = new Entity();
        }
        
        [Test, Performance]
        public void Add()
        {
            var collection = new EntityCollection<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        collection.Add(_source[i]);
                })
                .CleanUp(collection.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var collection = new EntityCollection<Entity>(_source);
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        collection.Contains(_source[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Contains()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var collection = new EntityCollection<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        collection.Remove(_source[i]);
                })
                .SetUp(() => collection.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Remove()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
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
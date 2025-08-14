using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public sealed class HashSet_Performance
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
        public void Contains()
        {
            var set = new HashSet<Entity>();
            set.UnionWith(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool _ = set.Contains(_source[i]);
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Contains()"))
                .Run();
        }

        [Test, Performance]
        public void Add()
        {
            var set = new HashSet<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool _ = set.Add(_source[i]);
                    }
                })
                .SetUp(set.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Add()"))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var set = new HashSet<Entity>();
            Measure
                .Method(set.Clear)
                .SetUp(() => set.UnionWith(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var set = new HashSet<Entity>();
            set.UnionWith(_source);

            Measure.Method(() =>
                {
                    foreach (Entity entity in set)
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
            var set = new HashSet<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool _ = set.Remove(_source[i]);
                    }
                })
                .SetUp(() => set.UnionWith(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Remove()"))
                .Run();
        }
    }
}
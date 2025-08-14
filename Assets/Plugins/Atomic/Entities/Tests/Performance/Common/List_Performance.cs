using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class List_Performance
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
            var list = new List<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++) 
                        list.Add(_source[i]);
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Add()"))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var list = new List<Entity>();
            Measure
                .Method(list.Clear)
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Clear()", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void Contains()
        {
            var list = new List<Entity>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool _ = list.Contains(_source[i]);
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Contains()"))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var list = new List<Entity>(_source);
            Measure.Method(() =>
                {
                    foreach (Entity entity in list)
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
            var list = new List<Entity>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        list.Remove(_source[i]);
                    }
                })
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Remove()"))
                .Run();
        }
    }
}
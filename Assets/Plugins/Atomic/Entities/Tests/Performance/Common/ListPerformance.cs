using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class ListPerformance
    {
        private static readonly object Dummy = new();
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = Dummy;
        }

        [Test, Performance]
        public void Add()
        {
            var list = new List<object>();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        list.Add(_source[i]);
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var list = new List<object>();

            Measure.Method(list.Clear)
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var list = new List<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = list.Contains(_source[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Contains()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var list = new List<object>(_source);

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
            var list = new List<object>();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        list.Remove(_source[i]);
                })
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Remove()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get()
        {
            var list = new List<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = list[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Indexer get", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Set()
        {
            var list = new List<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        list[i] = _source[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Indexer set", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ForLoop()
        {
            var list = new List<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                        _ = list[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.For loop traversal", SampleUnit.Microsecond))
                .Run();
        }
    }
}
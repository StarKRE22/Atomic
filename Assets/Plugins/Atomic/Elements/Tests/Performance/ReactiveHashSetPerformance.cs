#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace Atomic.Elements
{
    public sealed class ReactiveHashSetPerformance
    {
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = new object();
        }

        [Test, Performance]
        public void Add()
        {
            var set = new ReactiveHashSet<object>();
            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = set.Add(_source[i]);
                })
                .CleanUp(set.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveHashSet.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var set = new ReactiveHashSet<object>(_source);
            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        set.Contains(_source[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveHashSet.Contains() - missing", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var set = new ReactiveHashSet<object>(_source);
            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        set.Remove(_source[i]);
                })
                .SetUp(() => set.UnionWith(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveHashSet.Remove()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var set = new ReactiveHashSet<object>(_source);

            Measure.Method(() =>
                {
                    foreach (object unused in set)
                    {
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveHashSet.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var set = new ReactiveHashSet<object>(N);

            Measure.Method(set.Clear)
                .SetUp(() => set.UnionWith(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveHashSet.Clear()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
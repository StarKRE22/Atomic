#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    public sealed class HashSetPerformance
    {
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = new object();
        }

        [Test, Performance]
        public void Add()
        {
            var set = new HashSet<object>();
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++) 
                        _ = set.Add(_source[i]);
                })
                .SetUp(set.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var set = new HashSet<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool _ = set.Contains(_source[i]);
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Contains()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var set = new HashSet<object>();
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
            var set = new HashSet<object>(_source);

            Measure.Method(() =>
                {
                    foreach (object unused in set)
                    {
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var set = new HashSet<object>();
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
                .SampleGroup(new SampleGroup("HashSet.Remove()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
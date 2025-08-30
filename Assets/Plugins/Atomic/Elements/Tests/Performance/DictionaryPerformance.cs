#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    public sealed class DictionaryPerformance
    {
        private const int N = 1000;
        private static readonly object Dummy = new();
        private Dictionary<int, object> _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new Dictionary<int, object>(N);
            for (int i = 0; i < N; i++)
                _source.Add(i, Dummy);
        }

        [Test, Performance]
        public void Add()
        {
            var dict = new Dictionary<int, object>(N);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        dict.Add(i, Dummy);
                })
                .CleanUp(dict.Clear)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var dict = new Dictionary<int, object>(N);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        dict.Remove(i);
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        dict.Add(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ContainsKey()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = _source.ContainsKey(i);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            Measure.Method(() =>
                {
                    foreach (var kv in _source)
                    {
                        _ = kv.Key;
                        _ = kv.Value;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Set()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        _source[i] = Dummy;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void Indexer_Get()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = _source[i];
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get_SafeCast()
        {
            _source = new Dictionary<int, object>(N);
            for (int i = 0; i < N; i++)
                _source.Add(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        string _ = (string) _source[i];
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get_UnsafeCast()
        {
            _source = new Dictionary<int, object>(N);
            for (int i = 0; i < N; i++)
                _source.Add(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        object value = _source[i];
                        ref readonly string _ = ref UnsafeUtility.As<object, string>(ref value);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var dict = new Dictionary<int, object>(N);
            Measure.Method(dict.Clear)
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        dict.Add(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = _source.TryGetValue(i, out object _);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
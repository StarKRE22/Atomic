#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace Atomic.Elements
{
    public sealed class ReactiveDictionaryPerformance
    {
        private const int N = 1000;
        private static readonly object Dummy = new();
        private object[] _values;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _values = new object[N];

            for (int i = 0; i < N; i++) 
                _values[i] = Dummy;
        }

        private void Populate(IDictionary<int, object> dict)
        {
            dict.Clear();
            for (int i = 0; i < N; i++)
                dict.Add(i, _values[i]);
        }

        [Test, Performance]
        public void Add()
        {
            var dict = new ReactiveDictionary<int, object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.Add(i, _values[i]);
                })
                .CleanUp(dict.Clear)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Add()", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void Indexer_Set()
        {
            var dict = new ReactiveDictionary<int, object>();
            
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        dict[i] = Dummy;
                    }
                })
                .SetUp(() =>  Populate(dict))
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ContainsKey()
        {
            var dict = new ReactiveDictionary<int, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.ContainsKey(i);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.ContainsKey() - existing", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue()
        {
            var dict = new ReactiveDictionary<int, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = dict.TryGetValue(i, out _);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.TryGetValue() - existing", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get()
        {
            var dict = new ReactiveDictionary<int, object>();
            for (int i = 0; i < N; i++)
                dict.Add(i, _values[i]);
            
            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = dict[i];
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Indexer get", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var dict = new ReactiveDictionary<int, object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.Remove(i);
                })
                .SetUp(() => Populate(dict))
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Remove()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var dict = new ReactiveDictionary<int, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    foreach (KeyValuePair<int, object> kv in dict)
                    {
                        _ = kv.Key;
                        _ = kv.Value;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var dict = new ReactiveDictionary<int, object>();

            Measure.Method(dict.Clear)
                .SetUp(() => Populate(dict))
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Clear()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
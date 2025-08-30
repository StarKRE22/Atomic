#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace Atomic.Elements
{
    public sealed class ReactiveDictionaryPerformance
    {
        private const int N = 1000;

        private object[] _keys;
        private object[] _values;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _keys = new object[N];
            _values = new object[N];

            for (int i = 0; i < N; i++)
            {
                _keys[i] = new object();
                _values[i] = new object();
            }
        }

        private void Populate(IDictionary<object, object> dict)
        {
            dict.Clear();
            for (int i = 0; i < N; i++)
                dict.Add(_keys[i], _values[i]);
        }

        [Test, Performance]
        public void Add()
        {
            var dict = new ReactiveDictionary<object, object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.Add(_keys[i], _values[i]);
                })
                .CleanUp(dict.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Add()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void ContainsKey()
        {
            var dict = new ReactiveDictionary<object, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.ContainsKey(_keys[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.ContainsKey() - existing", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue()
        {
            var dict = new ReactiveDictionary<object, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.TryGetValue(_keys[i], out _);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.TryGetValue() - existing", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Get()
        {
            var dict = new ReactiveDictionary<object, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = dict[_keys[i]];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Indexer get", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var dict = new ReactiveDictionary<object, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        dict.Remove(_keys[i]);
                })
                .SetUp(() => Populate(dict))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Remove()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var dict = new ReactiveDictionary<object, object>();
            Populate(dict);

            Measure.Method(() =>
                {
                    foreach (KeyValuePair<object, object> kv in dict)
                    {
                        _ = kv.Key;
                        _ = kv.Value;
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Enumerator", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var dict = new ReactiveDictionary<object, object>();

            Measure.Method(dict.Clear)
                .SetUp(() => Populate(dict))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveDictionary.Clear()", SampleUnit.Millisecond))
                .Run();
        }
    }
}
#endif
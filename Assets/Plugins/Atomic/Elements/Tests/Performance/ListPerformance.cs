#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ListPerformance
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
            var absent = new object();
            
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = list.Contains(absent);
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
        public void RemoveAt()
        {
            var list = new List<object>();

            Measure.Method(() =>
                {
                    for (int i = N - 1; i >= 0; i--)
                        list.RemoveAt(i);
                })
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.RemoveAt()", SampleUnit.Microsecond))
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
        
        [Test, Performance]
        public void CopyTo()
        {
            var list = new List<object>(_source);
            object[] buffer = new object[N];

            Measure.Method(() =>
                {
                    list.CopyTo(buffer);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.CopyTo()", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void Insert()
        {
            var list = new List<object>();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        list.Insert(0, _source[i]); // вставляем в начало списка
                })
                .CleanUp(list.Clear) // очищаем список после каждого измерения
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Insert()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
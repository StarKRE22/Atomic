#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveListPerformance
    {
        private static readonly object Dummy = new();
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = Dummy;
        }

        [Test, Performance]
        public void Indexer_Get()
        {
            var list = new ReactiveList<object>(_source);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = list[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Indexer get", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void Indexer_Set()
        {
            var list = new ReactiveList<object>(N);
            
            object dummy = new object();
            for (var i = 0; i < N; i++)
                list.Add(dummy);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list[i] = Dummy;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Indexer set", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Add()
        {
            var list = new ReactiveList<object>(N);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list.Add(_source[i]);
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Add()", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void Remove()
        {
            var list = new ReactiveList<object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list.Remove(_source[i]);
                })
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Remove()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void RemoveAt()
        {
            var list = new ReactiveList<object>();

            Measure.Method(() =>
                {
                    for (int i = N - 1; i >= 0; i--)
                        list.RemoveAt(i);
                })
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.RemoveAt()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var list = new ReactiveList<object>(_source);

            Measure.Method(list.Clear)
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var list = new ReactiveList<object>(_source);

            Measure.Method(() =>
                {
                    foreach (var item in list)
                        _ = item;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void CopyTo()
        {
            var list = new ReactiveList<object>(_source);
            object[] buffer = new object[N];

            Measure.Method(() => list.CopyTo(buffer))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.CopyTo()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Insert()
        {
            var list = new ReactiveList<object>();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        list.Insert(0, _source[i]); // вставляем в начало
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Insert()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var list = new ReactiveList<object>(_source);
            var absent = new object();
            
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _ = list.Contains(absent);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.Contains()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ForLoop()
        {
            var list = new ReactiveList<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        _ = list[i];
                    }
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveList.For loop traversal", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveLinkedListPerformance
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
        public void Add()
        {
            var list = new ReactiveLinkedList<object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list.Add(_source[i]);
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Insert()
        {
            var list = new ReactiveLinkedList<object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list.Insert(0, _source[i]); // insert at head (O(1) for linked list)
                })
                .CleanUp(list.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Insert(0, item)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void RemoveAt()
        {
            var list = new ReactiveLinkedList<object>();

            Measure.Method(() =>
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                        list.RemoveAt(i);
                })
                .SetUp(() =>
                {
                    var l = new ReactiveLinkedList<object>(_source);
                    list = l;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.RemoveAt(end)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Remove()
        {
            var list = new ReactiveLinkedList<object>();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list.Remove(_source[i]);
                })
                .SetUp(() =>
                {
                    var l = new ReactiveLinkedList<object>(_source);
                    list = l;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Remove(item)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get()
        {
            var list = new ReactiveLinkedList<object>(_source);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = list[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Indexer get", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Set()
        {
            var list = new ReactiveLinkedList<object>(_source);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        list[i] = Dummy;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Indexer set", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var list = new ReactiveLinkedList<object>();

            Measure.Method(list.Clear)
                .SetUp(() => list.AddRange(_source))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var list = new ReactiveLinkedList<object>(_source);

            Measure.Method(() =>
                {
                    foreach (var item in list)
                        _ = item;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Enumerator", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void CopyTo()
        {
            var list = new ReactiveLinkedList<object>(_source);
            object[] buffer = new object[N];

            Measure.Method(() => list.CopyTo(buffer, 0))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.CopyTo()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Contains()
        {
            var list = new ReactiveLinkedList<object>(_source);
            var absent = new object();

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = list.Contains(absent);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.Contains()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ForLoop()
        {
            var list = new ReactiveLinkedList<object>(_source);

            Measure.Method(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                        _ = list[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveLinkedList.For loop traversal", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
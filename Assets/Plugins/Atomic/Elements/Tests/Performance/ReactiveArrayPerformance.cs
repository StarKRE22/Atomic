#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    public sealed class ReactiveArray_Performance
    {
        private const int N = 1000;
        private int[] _values;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _values = new int[N];
            for (int i = 0; i < N; i++)
                _values[i] = i;
        }

        private void Populate(ReactiveArray<int> array)
        {
            array.Clear();
            for (int i = 0; i < N; i++)
                array[i] = _values[i];
        }

        [Test, Performance]
        public void Indexer_Set()
        {
            var array = new ReactiveArray<int>(N);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        array[i] = _values[i];
                })
                .CleanUp(array.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Indexer set", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get()
        {
            var array = new ReactiveArray<int>(_values);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = array[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Indexer get", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Clear()
        {
            var array = new ReactiveArray<int>(_values);

            Measure.Method(array.Clear)
                .SetUp(() => Populate(array))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Clear()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Replace()
        {
            var array = new ReactiveArray<int>(N);

            Measure.Method(() =>
                {
                    array.Replace(_values);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Replace()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Fill()
        {
            var array = new ReactiveArray<int>(N);

            Measure.Method(() =>
                {
                    array.Fill(42);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Fill()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Resize()
        {
            var array = new ReactiveArray<int>(N);

            Measure.Method(() =>
                {
                    array.Resize(N + 500);
                    array.Resize(N);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Resize()", SampleUnit.Millisecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var array = new ReactiveArray<int>(_values);

            Measure.Method(() =>
                {
                    foreach (var item in array)
                        _ = item;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Enumerator", SampleUnit.Millisecond))
                .Run();
        }
    }

}
#endif
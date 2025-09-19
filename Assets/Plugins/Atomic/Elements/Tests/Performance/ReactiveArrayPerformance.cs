#if UNITY_6000
using System;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Elements
{
    public sealed class ReactiveArrayPerformance
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
            var array = new ReactiveArray<object>(_source);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        _ = array[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Indexer get", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Set()
        {
            var array = new ReactiveArray<object>(N);

            Measure.Method(() =>
                {
                    for (var i = 0; i < N; i++)
                        array[i] = Dummy;
                })
                .CleanUp(array.Clear)
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Indexer set", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Copy()
        {
            var array = new ReactiveArray<object>(_source);
            object[] destination = new object[N];

            Measure.Method(() => { array.CopyTo(0, destination, 0, N); })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Copy()", SampleUnit.Microsecond))
                .Run();
        }

        
        [Test, Performance]
        public void Clear()
        {
            var array = new ReactiveArray<object>(_source);

            Measure.Method(array.Clear)
                .SetUp(() => array.Fill(Dummy))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Clear", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Replace()
        {
            var array = new ReactiveArray<object>(N);

            Measure.Method(() => { array.Populate(_source); })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Replace()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Fill()
        {
            var array = new ReactiveArray<object>(N);

            Measure.Method(() => { array.Fill(42); })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Fill()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Resize()
        {
            var array = new ReactiveArray<object>(N);

            Measure.Method(() =>
                {
                    array.Resize(N + 500);
                    array.Resize(N);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Resize()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            var array = new ReactiveArray<object>(_source);

            Measure.Method(() =>
                {
                    foreach (object item in array)
                        _ = item;
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.Enumerator", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void ForLoop()
        {
            var array = new ReactiveArray<object>(_source);
            
            Measure.Method(() =>
                {
                    for (int i = 0, count = array.Length; i < count; i++) 
                        _ = array[i];
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("ReactiveArray.ForLoop", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
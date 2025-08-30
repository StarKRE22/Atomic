#if UNITY_6000
using System;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class ArrayPerformance
    {
        private static readonly object Dummy = new();
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = Dummy;
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
        public void Indexer_Set()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _source[i] = Dummy;
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.SetValue", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get_SafeCast()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++) 
                        _ = (string) _source[i];
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Indexer_Get_UnsafeCast()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        ref readonly string _ = ref UnsafeUtility.As<object, string>(ref _source[i]);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

   

        [Test, Performance]
        public void Copy()
        {
            var copy = new object[N];
            Measure.Method(() => Array.Copy(_source, copy, N))
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.Copy", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Enumerator()
        {
            Measure.Method(() =>
                {
                    foreach (var item in _source)
                        _ = item;
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.Foreach", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void ForLoop()
        {
            Measure.Method(() =>
                {
                    for (int i = 0, count = _source.Length; i < count; i++) 
                        _ = _source[i];
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.ForLoop", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
#if UNITY_6000
using System;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class ArrayPerformance
    {
        private const int N = 1000;
        private object[] _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new object[N];
            for (int i = 0; i < N; i++)
                _source[i] = "Sample";
        }

        [Test, Performance]
        public void GetValue()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        object _ = _source[i];
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void GetValue_SafeCast()
        {
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
        public void GetValue_UnsafeCast()
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
        public void SetValue()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        _source[i] = "NewValue";
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.SetValue", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void CopyToNewArray()
        {
            Measure.Method(() =>
                {
                    var copy = new object[N];
                    Array.Copy(_source, copy, N);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.CopyToNewArray", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void ForeachLoop()
        {
            Measure.Method(() =>
                {
                    foreach (var item in _source)
                        _ = item;
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Array.ForeachLoop", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
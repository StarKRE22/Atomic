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
        private Dictionary<int, object> _source;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new Dictionary<int, object>(N);
            for (int i = 0; i < N; i++)
                _source.Add(i, "Sample");
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
                        object value = _source[i];
                        ref readonly string _ = ref UnsafeUtility.As<object, string>(ref value);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif
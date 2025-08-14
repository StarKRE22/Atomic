#if UNITY_6000
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class Array_Performance
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
    }
}
#endif
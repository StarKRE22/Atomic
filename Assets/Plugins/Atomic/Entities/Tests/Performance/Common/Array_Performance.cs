#if UNITY_6000
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class Array_Performance
    {
        [Test, Performance]
        public void GetValue()
        {
            object[] array = new object[1000];

            for (int i = 0; i < 1000; i++)
                array[i] = new string("Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        object _ = array[i];
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
            object[] array = new object[1000];

            for (int i = 0; i < 1000; i++)
                array[i] = new string("Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string unused = (string) array[i];
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
            object[] array = new object[1000];

            for (int i = 0; i < 1000; i++)
                array[i] = new string("Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        object value = array[i];
                        ref readonly string unused = ref UnsafeUtility.As<object, string>(ref value);
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
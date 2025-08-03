#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class Dictionary_Performance
    {
        
        [Test, Performance]
        public void GetValue()
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            for (int i = 0; i < 1000; i++)
                dictionary.Add(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        object _ = dictionary[i];
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
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            for (int i = 0; i < 1000; i++)
                dictionary.Add(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string unused = (string) dictionary[i];
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
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            for (int i = 0; i < 1000; i++)
                dictionary.Add(i, "Sample");

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        object value = dictionary[i];
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
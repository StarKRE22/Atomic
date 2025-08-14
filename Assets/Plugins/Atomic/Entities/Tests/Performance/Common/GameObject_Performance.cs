#if UNITY_6000
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class GameObject_Performance
    {
        private const int N = 1000;
        private GameObject[] _source;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _source = new GameObject[N];
            for (int i = 0; i < N; i++)
                _source[i] = new GameObject($"{i}");
        }
        
        [Test, Performance]
        public void GetComponent()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        Transform _ = _source[i].GetComponent<Transform>();
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
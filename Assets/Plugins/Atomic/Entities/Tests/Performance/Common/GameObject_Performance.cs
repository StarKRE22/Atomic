#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace Atomic.Entities
{
    public class GameObject_Performance
    {
        [Test, Performance]
        public void GetComponent()
        {
            var gameObjects = new List<GameObject>();
            for (int i = 0; i < 1000; i++)
                gameObjects.Add(new GameObject($"{i}"));

            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        GameObject gameObject = gameObjects[i];
                        Transform unused = gameObject.GetComponent<Transform>();
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
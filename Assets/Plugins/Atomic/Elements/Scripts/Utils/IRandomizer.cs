using System;
using System.Threading;
using Random = UnityEngine.Random;

namespace Atomic.Elements
{
    public interface IRandomizer
    {
        int Range(int from, int to);

        float Range(float from, float to);
    }

    // public sealed class DefaultRandomizer : IRandomizer
    // {
    //     static readonly ThreadLocal<System.Random> random = new(() => 
    //         new System.Random(Environment.TickCount ^ Thread.CurrentThread.ManagedThreadId));
    //
    //     public int Range(int from, int to)
    //     {
    //         .Random r = random.Value;
    //         return r.Next(from, to);
    //     }
    //
    //     public float Range(float from, float to)
    //     {
    //         System.Random r = random.Value;
    //         return (float)(r.NextDouble() * (to - from) + from);
    //     }
    // }

    public sealed class UnityRandomizer : IRandomizer
    {
        public static readonly UnityRandomizer Instance = new();
        
        public int Range(int from, int to) =>
            Random.Range(from, to);

        public float Range(float from, float to) =>
            Random.Range(from, to);
    }
}
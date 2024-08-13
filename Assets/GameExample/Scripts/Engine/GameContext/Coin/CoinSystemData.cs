using System;
using Atomic.Elements;
using UnityEngine;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class CoinSystemData
    {
        public IEntityPool pool;
        public Bounds spawnArea;
        public Cycle spawnCycle;
    }
}
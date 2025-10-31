using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    [Serializable]
    public sealed class SpawnInfo
    {
        public SceneEntity prefab;
        public Transform container;
        public Bounds area = new(Vector3.zero, new Vector3(5, 0, 5));
        public Cooldown period = 2;
    }
}
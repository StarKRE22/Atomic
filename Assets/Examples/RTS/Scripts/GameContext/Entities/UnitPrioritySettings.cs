using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class UnitPrioritySettings : EntitySystemBase<IGameEntity>.Settings
    {
        [Header("Distance")]
        public float highDistance = 200f;
        public float mediumDistance = 400f;
    }
}

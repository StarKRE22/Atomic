using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class UnitPrioritySettings : GameEntitySystemSettings
    {
        [Header("Distance")]
        public float highDistance = 200f;
        public float mediumDistance = 400f;
    }
}

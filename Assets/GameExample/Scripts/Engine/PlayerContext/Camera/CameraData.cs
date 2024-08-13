using System;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class CameraData
    {
        public Transform transform;
        public float3 offset;
    }
}
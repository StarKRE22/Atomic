#if UNITY_EDITOR
using Atomic.Entities;
using UnityEditor;
using UnityEngine;

namespace RTSGame
{
    public sealed class TransformGizmos : IEntityGizmos<IUnit>
    {
        public void DrawGizmos(IUnit entity)
        {
            Vector3 center = entity.GetPosition().Value;
            float scale = entity.GetScale().Value;
            Handles.DrawWireDisc(center, Vector3.up, scale);
        }
    }
}
#endif
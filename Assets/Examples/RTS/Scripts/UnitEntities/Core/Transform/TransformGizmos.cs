using Atomic.Entities;
using UnityEditor;
using UnityEngine;

namespace RTSGame
{
    public sealed class TransformGizmos : IEntityGizmos<IUnitEntity>
    {
        public void DrawGizmos(IUnitEntity entity)
        {
            Vector3 center = entity.GetPosition().Value;
            float scale = entity.GetScale().Value;
            Handles.DrawWireDisc(center, Vector3.up, scale);
        }
    }
}
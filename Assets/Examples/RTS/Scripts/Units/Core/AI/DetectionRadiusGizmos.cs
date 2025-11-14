using Atomic.Entities;
using UnityEditor;
using UnityEngine;

namespace RTSGame
{
    public class DetectionRadiusGizmos : IEntityGizmos<IUnit>
    {
        public void DrawGizmos(IUnit entity)
        {
            Vector3 center = entity.GetPosition().Value;
            float scale = entity.GetDetectionRadius().Value;
            Handles.DrawWireDisc(center, Vector3.up, scale);
        }
    }
}
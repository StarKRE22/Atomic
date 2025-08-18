// using Atomic.Entities;
// using UnityEditor;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class RadiusGizmos : IEntityGizmos
//     {
//         public void OnGizmosDraw(in IEntity entity)
//         {
//             Vector3 center = entity.GetPosition().Value;
//             float radius = entity.GetRadius().Value;
//             Handles.DrawWireDisc(center, Vector3.up, radius);
//         }
//     }
// }
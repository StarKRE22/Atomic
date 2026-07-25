// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class SpatialHashGizmos : IEntityGizmos<IGameContext>
//     {
//         private const float Y_OFFSET = 0.05f;
//
//         public void DrawGizmos(IGameContext gameContext)
//         {
//             if (!gameContext.TryGetEntitySpace(out var hash) || hash == null)
//                 return;
//
//             float cellSize = hash.CellSize;
//             foreach ((Vector2Int cell, int count) cell in hash.GetAllCells())
//             {
//                 Vector2Int coord = cell.cell;
//                 int count = cell.count;
//
//                 Vector3 center = new Vector3(
//                     (coord.x + 0.5f) * cellSize,
//                     Y_OFFSET,
//                     (coord.y + 0.5f) * cellSize
//                 );
//
//                 Vector3 size = new Vector3(cellSize, 0.01f, cellSize);
//
//                 float t = Mathf.Clamp01(count / 10f);
//                 Gizmos.color = Color.Lerp(Color.green, Color.red, t);
//                 Gizmos.DrawCube(center, size);
//
//                 Gizmos.color = Color.black;
//                 Gizmos.DrawWireCube(center, size);
//             }
//         }
//     }
// }
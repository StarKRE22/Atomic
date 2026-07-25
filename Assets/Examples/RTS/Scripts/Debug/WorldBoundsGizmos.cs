#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RTSGame.Debugging
{
    public sealed class WorldBoundsGizmos : MonoBehaviour
    {
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeZ;
        [SerializeField] private float _cellSize = 1f;

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            DrawBounds(_sizeX, _sizeZ, _cellSize);
#endif
        }

#if UNITY_EDITOR
        private void DrawBounds(int sizeX, int sizeZ, float cellSize)
        {
            float width = sizeX * cellSize;
            float length = sizeZ * cellSize;

            Vector3 center = new Vector3(width * 0.5f, 0.05f, length * 0.5f);
            Vector3 size = new Vector3(width, 0f, length);

            Handles.color = Color.black;

            // 🔥 толщина линии
            float thickness = 4f;

            Handles.DrawWireCube(center, size);

            // 💣 жирная версия (реально толстые линии)
            Vector3 half = size * 0.5f;

            Vector3[] points =
            {
                center + new Vector3(-half.x, 0, -half.z),
                center + new Vector3( half.x, 0, -half.z),
                center + new Vector3( half.x, 0,  half.z),
                center + new Vector3(-half.x, 0,  half.z)
            };

            for (int i = 0; i < 4; i++)
            {
                Vector3 a = points[i];
                Vector3 b = points[(i + 1) % 4];

                Handles.DrawAAPolyLine(thickness, a, b);
            }
        }
#endif
    }
}
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CoinSpawnAreaGizmos : IEntityGizmos<IGameContext>
    {
        public void DrawGizmos(IGameContext context)
        {
            Bounds bounds = context.GetCoinSpawnArea();

            Color prevColor = Gizmos.color;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
            Gizmos.color = prevColor;
        }
    }
}
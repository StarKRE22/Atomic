using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class EntitySpawnInfo
    {
        public GameEntityType entityType;
        public Vector2Int[] points;
    }
}
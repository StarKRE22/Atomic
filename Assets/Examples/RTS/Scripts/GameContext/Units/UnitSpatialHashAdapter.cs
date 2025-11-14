using UnityEngine;

namespace RTSGame
{
    public sealed class UnitSpatialHashAdapter : SpatialHash<IUnit>.IAdapter
    {
        public Vector3 GetPosition(IUnit item) => item.GetPosition().Value;
    }
}
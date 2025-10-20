using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "UnitMultiFactory",
        menuName = "RTSGame/Units/New UnitMultiFactory"
    )]
    public sealed class UnitMultiFactory : ScriptableMultiEntityFactory<string, IUnitEntity, UnitFactory>
    {
        protected override string GetKey(UnitFactory factory) => factory.Name;
    }
}
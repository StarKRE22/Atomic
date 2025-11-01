using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "UnitCatalog",
        menuName = "RTSGame/Units/New UnitCatalog"
    )]
    public sealed class UnitCatalog : ScriptableMultiEntityFactory<string, IUnitEntity, UnitFactory>
    {
        protected override string GetKey(UnitFactory factory) => factory.Name;
    }
}
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "UnitCatalog",
        menuName = "RTSGame/Units/New UnitCatalog"
    )]
    public sealed class UnitCatalog : ScriptableMultiEntityFactory<string, IUnit, UnitFactory>
    {
        protected override string GetKey(UnitFactory factory) => factory.Name;
    }
}
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "UnitViewCatalog",
        menuName = "RTSGame/Units/New UnitViewCatalog"
    )]
    public sealed class UnitViewCatalog : EntityViewCatalog<IUnit, UnitView>
    {
    }
}
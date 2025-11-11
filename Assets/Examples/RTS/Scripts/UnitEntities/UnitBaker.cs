using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [RequireComponent(typeof(UnitView))]
    public abstract class UnitBaker : SceneEntityBakerOptimized<IUnitEntity, UnitView>
    {
    }
    
    // [RequireComponent(typeof(UnitView))]
    // public abstract class UnitBaker : SceneEntityBaker<IUnitEntity>
    // {
    // }
}
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [RequireComponent(typeof(UnitView))]
    public abstract class UnitBaker : MonoEntityBakerOptimized<string, IUnit, UnitView, NoArgs>
    {
    }
}
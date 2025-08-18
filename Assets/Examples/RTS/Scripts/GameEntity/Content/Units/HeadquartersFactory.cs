using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "HeadquartersFactory",
        menuName = "RTSGame/Entities/New HeadquartersFactory"
    )]
    public sealed class HeadquartersFactory : GameEntityFactory
    {
        [SerializeField]
        private int _health;

        protected override void Install(IGameEntity entity)
        {
            entity.AddUnitTag();
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health));
            entity.AddPosition(new ReactiveVector3());
            entity.AddRotation(new ReactiveQuaternion());
            entity.AddTeam(new ReactiveVariable<TeamType>());
            entity.AddBehaviour<DeathBehaviour>();
        }
    }
}


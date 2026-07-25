using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "ProjectileFactory",
        menuName = "RTSGame/GameEntities/New ProjectileFactory"
    )]
    public sealed class ProjectileFactory : GameEntityFactory
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<int> _damage;

        [SerializeField]
        private float _lifetime;

        [SerializeField]
        private TransformEntityInstaller _transformInstaller;

        protected override void Install(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddProjectileTag();
            
            _transformInstaller.Install(entity);
            
            entity.AddDamage(_damage);
            entity.AddMoveSpeed(_moveSpeed);
            
            Cooldown cooldown = new Cooldown(_lifetime);
            entity.AddLifetime(cooldown);
            entity.AddTarget(new ReactiveVariable<IGameEntity>());
            entity.AddTeam(new ReactiveVariable<TeamType>());
            entity.WhenEnable(cooldown.ResetTime);
            entity.AddUpdatePriority(new ReactiveVariable<EntityUpdatePriority>(EntityUpdatePriority.High));
            
            // entity.AddBehaviour(new ProjectileLifetimeBehaviour(gameContext));
            // entity.AddBehaviour(new ProjectileMoveBehaviour(gameContext));
        }
    }
}
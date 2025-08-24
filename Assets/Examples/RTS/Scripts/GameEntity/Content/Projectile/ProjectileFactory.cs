using Atomic.Elements;
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

        public override IGameEntity Create()
        {
            IGameEntity entity = base.Create();
            
            IGameContext context = EntryPoint.GameContext;
            entity.AddProjectileTag();
            
            _transformInstaller.Install(entity);
            
            entity.AddDamage(_damage);
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddLifetime(new Cooldown(_lifetime));
            entity.AddTarget(new ReactiveVariable<IGameEntity>());

            entity.AddBehaviour(new ProjectileLifetimeBehaviour(context));
            entity.AddBehaviour(new ProjectileMoveBehaviour(context));
            
            return entity;
        }
    }
}
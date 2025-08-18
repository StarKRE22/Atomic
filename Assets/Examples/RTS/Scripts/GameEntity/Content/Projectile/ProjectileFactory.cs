using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "ProjectileFactory",
        menuName = "RTSGame/New ProjectileFactory"
    )]
    public sealed class ProjectileFactory : ScriptableEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<int> _damage;

        [SerializeField]
        private float _lifetime;

        [SerializeField]
        private Const<float> _radius;

        protected override void Install(IGameEntity entity)
        {
            entity.AddProjectileTag();

            entity.AddDamage(_damage);
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddLifetime(new Cooldown(_lifetime));
            entity.AddTarget(new ReactiveVariable<IEntity>());

            entity.AddBehaviour<ProjectileLifetimeBehaviour>();
            entity.AddBehaviour<ProjectileFollowBehaviour>();
        }
    }
}
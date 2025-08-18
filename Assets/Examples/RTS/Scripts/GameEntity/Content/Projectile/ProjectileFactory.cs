using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "ProjectileInstaller",
        menuName = "SampleGame/Entities/New ProjectileInstaller"
    )]
    public sealed class ProjectileFactory : ScriptableEntityInstaller
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<int> _damage;

        [SerializeField]
        private float _lifetime;

        [SerializeField]
        private Const<float> _radius;

        public override void Install(IEntity entity)
        {
            entity.AddProjectileTag();

            entity.AddRadius(_radius);
            entity.AddDamage(_damage);
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddLifetime(new Cooldown(_lifetime));
            entity.AddTarget(new ReactiveVariable<IEntity>());

            entity.AddBehaviour<ProjectileLifetimeBehaviour>();
            entity.AddBehaviour<ProjectileFollowBehaviour>();

#if UNITY_EDITOR
            entity.AddBehaviour<RadiusGizmos>();
#endif
        }
    }
}
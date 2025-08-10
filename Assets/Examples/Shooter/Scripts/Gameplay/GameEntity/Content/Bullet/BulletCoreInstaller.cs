using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletCoreInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<int> _damage = 1;
        
        [SerializeField]
        private TriggerEvents _trigger;

        [SerializeField]
        private Cooldown _lifetime = 5;

        public override void Install(IEntity entity)
        {
            entity.AddTransform(this.transform);
            entity.AddGameObject(this.gameObject);

            //Lifetime
            entity.AddLifetime(new Cooldown(_lifetime, _lifetime));
            entity.AddBehaviour<BulletLifetimeBehaviour>();

            //Team
            entity.AddTeam(new ReactiveVariable<TeamType>(TeamType.NEUTRAL));
            entity.AddBehaviour<TeamLayerBehaviour>();
            
            //Move
            entity.AddMoveSpeed(new Const<float>(_moveSpeed));
            entity.AddBehaviour<BulletMoveBehaviour>();
            
            //Collision
            entity.AddTrigger(_trigger);
            entity.AddDamage(new Const<int>(_damage));
            entity.AddDestroyAction(new BaseAction(() => SpawnBulletUseCase.UnspawnBullet(GameContext.Instance, entity)));
            entity.AddBehaviour<BulletCollisionBehaviour>();
            // entity.AddDestroyAction(new BaseAction(() => Destroy(this.gameObject)));
        }
    }
}
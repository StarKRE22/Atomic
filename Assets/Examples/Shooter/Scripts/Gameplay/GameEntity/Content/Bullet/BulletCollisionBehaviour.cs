using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletCollisionBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private IEntity _entity;
        private TriggerEvents _trigger;
        private IValue<int> _damage;
        private IAction _destroyAction;
        private readonly IGameContext _gameContext;

        public BulletCollisionBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _destroyAction = entity.GetDestroyAction();
            _damage = entity.GetDamage();
            _trigger = entity.GetTrigger();

            _trigger.OnEntered += this.OnTriggerEntered;
        }

        public void OnDespawn(IEntity entity)
        {
            _trigger.OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            DamageArgs args = new DamageArgs
            {
                source = _entity,
                damage = _damage.Value
            };
            
            if (TakeDamageUseCase.TakeDamage(collider, args, _gameContext))
                _destroyAction.Invoke();
        }

    }
}
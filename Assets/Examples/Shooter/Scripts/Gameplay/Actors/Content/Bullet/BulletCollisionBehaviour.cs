using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletCollisionBehaviour : IEntityInit<IActor>, IEntityEnable, IEntityDisable
    {
        private IActor _entity;
        private TriggerEvents _trigger;
        private IValue<int> _damage;
        private IAction _destroyAction;
        private readonly IGameContext _gameContext;

        public BulletCollisionBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IActor entity)
        {
            _entity = entity;
            _destroyAction = entity.GetDestroyAction();
            _damage = entity.GetDamage();
            _trigger = entity.GetTrigger();
        }

        public void Enable(IEntity entity)
        {
            _trigger.OnEntered += this.OnTriggerEntered;
        }

        public void Disable(IEntity entity)
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

            collider.TakeDamage(args, _gameContext);
            _destroyAction.Invoke();
        }
    }
}
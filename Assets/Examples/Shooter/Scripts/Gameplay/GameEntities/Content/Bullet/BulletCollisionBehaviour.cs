using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletCollisionBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private readonly IGameContext _gameContext;

        private IGameEntity _entity;
        private TriggerEvents _trigger;
        private IValue<int> _damage;
        private IAction _destroyAction;

        public BulletCollisionBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _destroyAction = entity.GetDestroyAction();
            _damage = entity.GetDamage();
            _trigger = entity.GetTrigger();
            _trigger.OnEntered += this.OnTriggerEntered;
        }

        public void Dispose(IGameEntity entity)
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

            CombatUseCase.TakeDamage(collider, args, _gameContext);
            _destroyAction.Invoke();
        }
    }
}
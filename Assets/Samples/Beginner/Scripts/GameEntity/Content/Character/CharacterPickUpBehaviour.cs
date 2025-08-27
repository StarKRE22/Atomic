using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterPickUpBehaviour : IEntitySpawn<IGameEntity>, IEntityActivate, IEntityDeactivate
    {
        private TriggerEvents _triggerEvents;
        private IGameEntity _entity;
        private IGameContext _gameContext;

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _gameContext = GameContext.Instance;
            _triggerEvents = entity.GetTriggerEvents();
        }

        public void OnActivate(IEntity entity) => 
            _triggerEvents.OnEntered += this.OnTriggerEntered;

        public void OnDeactivate(IEntity entity) => 
            _triggerEvents.OnEntered -= this.OnTriggerEntered;

        private void OnTriggerEntered(Collider collider) => 
            CoinUseCase.Collect(_gameContext, _entity, collider);
    }
}
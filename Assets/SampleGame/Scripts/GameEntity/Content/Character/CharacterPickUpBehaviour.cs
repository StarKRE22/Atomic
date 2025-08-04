using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class CharacterPickUpBehaviour : IEntitySpawn<IGameEntity>, IEntityActive, IEntityInactive
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

        public void OnActive(IEntity entity)
        {
            _triggerEvents.OnEntered += this.OnTriggerEntered;
        }

        public void OnInactive(IEntity entity)
        {
            _triggerEvents.OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            CollectCoinUseCase.TryCollectCoin(_gameContext, _entity, collider);
        }
    }
}
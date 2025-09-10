using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterPickUpBehaviour : IEntityInit<IGameEntity>, IEntityEnable, IEntityDisable
    {
        private TriggerEvents _triggerEvents;
        private IGameEntity _entity;
        private IGameContext _gameContext;

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _gameContext = GameContext.Instance;
            _triggerEvents = entity.GetTriggerEvents();
        }

        public void Enable(IEntity entity) => 
            _triggerEvents.OnEntered += this.OnTriggerEntered;

        public void Disable(IEntity entity) => 
            _triggerEvents.OnEntered -= this.OnTriggerEntered;

        private void OnTriggerEntered(Collider collider) => 
            CoinsUseCase.Collect(_gameContext, _entity, collider);
    }
}
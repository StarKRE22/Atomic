using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CollectBehaviour : IEntityInit<IGameEntity>, IEntityEnable, IEntityDisable
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
        
        public static bool Collect(IGameContext context, IGameEntity character, Collider other) =>
            other.TryGetComponent(out IGameEntity entity) &&
            Collect(context, character, entity);

        public static bool Collect(IGameContext context, IGameEntity character, IGameEntity coin)
        {
            if (!coin.HasCoinTag())
                return false;
            
            //Earn money:
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(context, character);
            playerContext.GetMoney().Value += coin.GetMoney().Value;

            //Despawn coin:
            context.GetCoinPool().Return(coin);
            return true;
        }
    }
}
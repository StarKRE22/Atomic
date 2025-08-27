using System;
using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class MoneyPresenter : IEntitySpawn<IGameEntity>, IEntityActivate, IEntityDespawn
    {
        private MoneyView _view;
        private IReactiveValue<int> _money;

        public void OnSpawn(IGameEntity entity)
        {
            _view = entity.GetMoneyView();
            
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(GameContext.Instance, entity);
            _money = playerContext.GetMoney();
            _money.Subscribe(this.OnMoneyChanged);
        }

        public void OnActivate(IEntity entity)
        {
            _view.SetMoney($"Money: {_money.Value}");
        }

        public void OnDespawn(IEntity entity)
        {
            _money.Unsubscribe(this.OnMoneyChanged);
        }

        private void OnMoneyChanged(int money)
        {
            _view.ChangeMoney($"Money: {money}");
        }
    }
}
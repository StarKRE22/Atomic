using System;
using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class MoneyPresenter : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private MoneyView _view;
        private IReactiveValue<int> _money;

        public void OnSpawn(IGameEntity entity)
        {
            _view = entity.GetMoneyView();
            
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(GameContext.Instance, entity);
            _money = playerContext.GetMoney();
            _money.Observe(this.OnMoneyChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _money.Unsubscribe(this.OnMoneyChanged);
        }

        private void OnMoneyChanged(int money)
        {
            _view.SetMoney(money, "Money: {0}");
        }
    }
}
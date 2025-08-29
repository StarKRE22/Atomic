using System;
using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class MoneyPresenter : IEntityInit<IGameEntity>, IEntityEnable, IEntityDispose
    {
        private MoneyView _view;
        private IReactiveValue<int> _money;

        public void Init(IGameEntity entity)
        {
            _view = entity.GetMoneyView();
            
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(GameContext.Instance, entity);
            _money = playerContext.GetMoney();
            _money.Subscribe(this.OnMoneyChanged);
        }

        public void Enable(IEntity entity)
        {
            _view.SetMoney($"Money: {_money.Value}");
        }

        public void Dispose(IEntity entity)
        {
            _money.Unsubscribe(this.OnMoneyChanged);
        }

        private void OnMoneyChanged(int money)
        {
            _view.ChangeMoney($"Money: {money}");
        }
    }
}
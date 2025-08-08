using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class MoneyPresenter : IEntitySpawn<IUIContext>, IEntityDespawn
    {
        private readonly TeamType _teamType;

        private IReactiveValue<int> _money;
        private MoneyView _view;

        public MoneyPresenter(TeamType teamType)
        {
            _teamType = teamType;
        }

        public void OnSpawn(IUIContext context)
        {
            _view = context.GetMoneyView();
            
            IPlayerContext player = PlayersUseCase.GetPlayerFor(GameContext.Instance, _teamType);
            _money = player.GetMoney();
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
using Atomic.Contexts;
using Atomic.Entities;

namespace GameExample.Engine
{
    public static class CollectCoinUseCase
    {
        public static bool CollectCoin(this IContext playerContext, in IEntity coin)
        {
            if (!coin.HasCoinTag())
            {
                return false;
            }

            playerContext.EarnMoney(coin.GetMoney().Value);

            IContext gameContext = playerContext.Parent;
            gameContext.DestroyCoin(coin);
            return true;
        }
    }
}
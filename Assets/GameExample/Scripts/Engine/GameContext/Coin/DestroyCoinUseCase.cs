using Atomic.Contexts;
using Atomic.Entities;

namespace GameExample.Engine
{
    public static class DestroyCoinUseCase
    {
        public static void DestroyCoin(this IContext gameContext, IEntity coin)
        {
            gameContext.GetCoinSystemData().pool.Return(coin);
        }
    }
}
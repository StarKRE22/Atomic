using Atomic.Contexts;

namespace GameExample.Engine
{
    public static class PlayerMoneyUseCase
    {
        public static void EarnMoney(this IContext playerContext, in int money)
        {
            playerContext.GetMoney().Value += money;
        }
    }
}
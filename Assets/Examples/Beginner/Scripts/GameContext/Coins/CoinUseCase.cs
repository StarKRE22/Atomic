using UnityEngine;

namespace BeginnerGame
{
    public static class CoinUseCase
    {
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

        public static IGameEntity Spawn(IGameContext gameContext)
        {
            IGameEntity coin = gameContext.GetCoinPool().Rent();
            coin.GetPosition().Value = RandomSpawnPosition(gameContext);
            return coin;
        }

        public static Vector3 RandomSpawnPosition(IGameContext gameContext)
        {
            Bounds area = gameContext.GetCoinSpawnArea();
            Vector3 min = area.min;
            Vector3 max = area.max;
            return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        }
    }
}
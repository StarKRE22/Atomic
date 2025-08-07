using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public static class CoinUseCase
    {
        public static bool Collect(IGameContext context, IGameEntity character, Collider other) =>
            other.TryGetComponent(out IGameEntity entity) && entity.HasCoinTag() &&
            Collect(context, character, entity);

        public static bool Collect(IGameContext context, IGameEntity character, IGameEntity coin)
        {
            //Earn money:
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(context, character);
            playerContext.GetMoney().Value += coin.GetMoney().Value;

            //Despawn coin:
            context.GetCoinPool().Return(coin);
            return true;
        }

        public static IEntity Spawn(IGameContext gameContext)
        {
            IGameEntity coin = gameContext.GetCoinPool().Rent();

            Vector3 spawnPoint = RandomSpawnPoint(gameContext);
            coin.GetTransform().position = spawnPoint;
            return coin;
        }

        private static Vector3 RandomSpawnPoint(IGameContext gameContext)
        {
            Bounds area = gameContext.GetCoinSpawnArea();
            Vector3 min = area.min;
            Vector3 max = area.max;
            return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        }
    }
}
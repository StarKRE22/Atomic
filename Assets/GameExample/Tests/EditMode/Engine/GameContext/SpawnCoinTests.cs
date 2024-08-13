using Atomic.Contexts;
using Atomic.Entities;
using Atomic.Extensions;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class SpawnCoinTests
    {
        [Test]
        public void SpawnCoin()
        {
            //Arrange:
            var gameContext = CreateGameContext();
            var spawnPoint = new float3(10, 0, 10);

            //Act:
            IEntity coin = gameContext.SpawnCoin(spawnPoint);

            //Assert:
            Assert.AreEqual(spawnPoint, coin.GetPosition().Value);
        }

        private static IContext CreateGameContext()
        {
            var gameContext = new Context();
            gameContext.AddCoinSystemData(new CoinSystemData
            {
                spawnArea = new Bounds(Vector3.zero, new Vector3(5, 0, 5)),
                pool = new EntityPoolMock()
            });
            return gameContext;
        }
        
        private sealed class EntityPoolMock : IEntityPool
        {
            public IEntity Rent()
            {
                Entity coin = new Entity("Coin");
                coin.AddCoinTag();
                coin.AddPosition(new float3Reactive());
                return coin;
            }

            public void Return(IEntity entity)
            {
            }
        }
    }
}
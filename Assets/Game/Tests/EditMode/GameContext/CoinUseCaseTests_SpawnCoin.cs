using Atomic.Entities;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    [TestFixture]
    public partial class CoinUseCaseTests
    {
        [Test]
        public void SpawnCoin_Successfully()
        {
            var gameContext = new Context();
            gameContext.AddCoinSystemData(new CoinSystemData
            {
                spawnArea = new Bounds(Vector3.zero, new Vector3(5, 0, 5)),
                pool = new EntityPoolMock<IGameEntity>
                {
                    RentMethod = () =>
                    {
                        IGameEntity coin = new TestGameEntity();
                        coin.AddCoinTag();
                        coin.AddPosition(new float3Reactive());
                        return coin;
                    }
                }
            });
            return gameContext;
            
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
           
        }
        
        private sealed class EntityPoolMock : IEntityPool
        {
            public IEntity Rent()
            {
             
            }

            public void Return(IEntity entity)
            {
            }
        }
    }
}
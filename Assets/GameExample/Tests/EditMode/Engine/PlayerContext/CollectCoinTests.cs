using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using NUnit.Framework;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class CollectCoinTests
    {
        [Test]
        public void CollectCoin()
        {
            //Arrange:
            var coin = new Entity("Coin");
            coin.AddCoinTag();
            coin.AddMoney(new Const<int>(10));
            
            var gameContext = new Context();
            gameContext.AddCoinSystemData(new CoinSystemData
            {
                pool = new EntityPoolMock()
            });
            
            var playerContext = new Context("Player", gameContext);
            playerContext.AddMoney(100);

            //Act:
            bool success = playerContext.CollectCoin(coin);
            
            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(110, playerContext.GetMoney().Value);
        }

        [Test]
        public void WhenCollectNotCoinThenFalse()
        {
            //Arrange:
            var coin = new Entity("Coin");
            coin.AddMoney(new Const<int>(10));
            
            var entityWorld = new EntityWorld(coin);
            var gameContext = new Context();
            gameContext.AddCoinSystemData(new CoinSystemData
            {
                pool = new EntityPoolMock()
            });
            
            var playerContext = new Context("Player", gameContext);
            playerContext.AddMoney(100);

            //Act:
            bool success = playerContext.CollectCoin(coin);
            
            //Assert
            Assert.IsFalse(success);
            Assert.AreEqual(100, playerContext.GetMoney().Value);
            Assert.IsTrue(entityWorld.HasEntity(coin));
        }

        private sealed class EntityPoolMock : IEntityPool
        {
            public IEntity Rent()
            {
                throw new NotImplementedException();
            }

            public void Return(IEntity entity)
            {
            }
        }
    }
}
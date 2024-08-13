using System;
using System.Collections.Generic;
using Atomic.Contexts;
using Atomic.Entities;
using NUnit.Framework;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class DestroyCoinTests
    {
        [Test]
        public void DestroyCoin()
        {
            //Arrange:
            var coin = new Entity("Coin");
            
            var entityWorld = new EntityWorld(coin);
            var gameContext = CreateGameContext();

            //Act:
            gameContext.DestroyCoin(coin);

            //Assert:
            Assert.IsTrue((gameContext.GetCoinSystemData().pool as EntityPoolMock)!.Contains(coin));
        }

        private static IContext CreateGameContext()
        {
            var gameContext = new Context();
            gameContext.AddCoinSystemData(new CoinSystemData
            {
                pool = new EntityPoolMock()
            });
            return gameContext;
        }
        
        private sealed class EntityPoolMock : IEntityPool
        {
            private readonly HashSet<IEntity> pool = new();

            public IEntity Rent()
            {
                throw new NotImplementedException();
            }

            public bool Contains(IEntity entity)
            {
                return this.pool.Contains(entity);
            }

            public void Return(IEntity entity)
            {
                this.pool.Add(entity);
            }
        }
    }
}
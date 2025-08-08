using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using NUnit.Framework;

namespace SampleGame
{
    [TestFixture]
    public partial class CoinUseCaseTests
    {
        [Test]
        public void CollectCoin_Successful()
        {
            //Arrange:
            var coin = new TestGameEntity();
            coin.AddCoinTag();
            coin.AddMoney(new Const<int>(10));

            var character = new TestGameEntity();
            character.AddCharacterTag();
            character.AddTeamType(new ReactiveVariable<TeamType>(TeamType.BLUE));

            var playerContext = new TestPlayerContext();
            playerContext.AddMoney(new ReactiveVariable<int>(100));
            
            var gameContext = new TestGameContext();
            gameContext.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, playerContext}
            });

            bool coinReturned = false;
            gameContext.AddCoinPool(new EntityPoolMock<IGameEntity>
            {
                ReturnMethod = _ => coinReturned = true
            });
            
            //Act:
            bool success = CoinUseCase.Collect(gameContext, character, coin);
            
            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(110, playerContext.GetMoney().Value);
            Assert.IsTrue(coinReturned);
        }

        [Test]
        public void CollectCoin_EntityNotCoin_ReturnsFalse()
        {
            //Arrange:
            var coin = new TestGameEntity();
            coin.AddMoney(new Const<int>(10));

            var character = new TestGameEntity();
            character.AddCharacterTag();
            character.AddTeamType(new ReactiveVariable<TeamType>(TeamType.BLUE));

            var playerContext = new TestPlayerContext();
            playerContext.AddMoney(new ReactiveVariable<int>(100));
            
            var gameContext = new TestGameContext();
            gameContext.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, playerContext}
            });
            
            bool coinReturned = false;
            gameContext.AddCoinPool(new EntityPoolMock<IGameEntity>
            {
                ReturnMethod = _ => coinReturned = true
            });
            
            //Act:
            bool success = CoinUseCase.Collect(gameContext, character, coin);
            
            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(100, playerContext.GetMoney().Value);
            Assert.IsFalse(coinReturned);
        }
    }
}
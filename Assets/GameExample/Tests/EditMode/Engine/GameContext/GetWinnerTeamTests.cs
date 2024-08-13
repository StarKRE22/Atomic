using System.Collections.Generic;
using Atomic.Contexts;
using NUnit.Framework;

namespace GameExample.Engine
{
    [TestFixture]
    public sealed class GetWinnerTeamTests
    {
        [TestCase(10, 1, ExpectedResult = TeamType.BLUE)]
        [TestCase(10, 11, ExpectedResult = TeamType.RED)]
        [TestCase(10, 10, ExpectedResult = TeamType.NONE)]
        public TeamType GetWinner(int blueMoney, int redMoney)
        {
            var gameContext = new Context("GameContext");
            gameContext.AddPlayerMap(new Dictionary<TeamType, IContext>
            {
                {TeamType.BLUE, this.CreatePlayerContext(blueMoney)},
                {TeamType.RED, this.CreatePlayerContext(redMoney)}
            });

            return gameContext.GetWinnerPlayerTeam();
        }

        private IContext CreatePlayerContext(int money)
        {
            Context playerContext = new Context("Player");
            playerContext.AddMoney(money);
            return playerContext;
        }
    }
}
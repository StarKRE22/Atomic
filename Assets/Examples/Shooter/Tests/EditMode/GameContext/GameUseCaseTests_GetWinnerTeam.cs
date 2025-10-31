// using System.Collections.Generic;
// using Atomic.Elements;
// using NUnit.Framework;
// using BeginnerGame;
//
// namespace GameExample.Engine
// {
//     [TestFixture]
//     public sealed partial class GameUseCaseTests_GetWinnerTeam
//     {
//         [TestCase(10, 1, ExpectedResult = TeamType.BLUE)]
//         [TestCase(10, 11, ExpectedResult = TeamType.RED)]
//         [TestCase(10, 10, ExpectedResult = TeamType.NONE)]
//         public TeamType GetWinner(int blueMoney, int redMoney)
//         {
//             var gameContext = new TestGameContext();
//             gameContext.AddPlayers(new Dictionary<TeamType, IPlayerContext>
//             {
//                 {TeamType.BLUE, this.CreatePlayerContext(blueMoney)},
//                 {TeamType.RED, this.CreatePlayerContext(redMoney)}
//             });
//
//             return GameOverUseCase.GetWinnerTeam(gameContext);
//         }
//
//         private IPlayerContext CreatePlayerContext(int money)
//         {
//             IPlayerContext playerContext = new TestPlayerContext();
//             playerContext.AddMoney(new ReactiveVariable<int>(money));
//             return playerContext;
//         }
//     }
// }
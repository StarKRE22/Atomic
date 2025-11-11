// namespace ShooterGame.Examples.Shooter.Scripts.Gameplay.GameOver
// {
//     public class GameOver
//     {
//         private void OnGameTimeFinished()
//         {
//             context.GetWinnerTeam().Value = GetWinnerTeam(context);
//             context.GetGameOverEvent().Invoke();
//             
//             context.Disable();
//             Debug.Log("Game Over!");
//         }
//         
//         
//         public static void GameOver(IGameContext context)
//         {
//             
//         }
//
//         public static TeamType GetWinnerTeam(IGameContext context)
//         {
//             IDictionary<TeamType, IPlayerContext> players = context.GetPlayers();
//             int redMoney = players[TeamType.RED].GetMoney().Value;
//             int blueMoney = players[TeamType.BLUE].GetMoney().Value;
//
//             return redMoney > blueMoney
//                 ? TeamType.RED
//                 : blueMoney > redMoney
//                     ? TeamType.BLUE
//                     : TeamType.NONE;
//         }
//
//     }
// }
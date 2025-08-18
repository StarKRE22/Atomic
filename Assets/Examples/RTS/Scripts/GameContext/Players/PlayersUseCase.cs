// using System.Collections.Generic;
// using Atomic.Entities;
//
// namespace RTSGame
// {
//     public static class PlayersUseCase
//     {
//         public static PlayerContext GetPlayerContext(in GameContext gameContext, in IEntity unit)
//         {
//             TeamType teamType = unit.GetTeam().Value;
//             return GetPlayerContext(gameContext, teamType);
//         }
//
//         public static PlayerContext GetPlayerContext(in GameContext gameContext, in TeamType player)
//         {
//             IDictionary<TeamType, PlayerContext> players = gameContext.GetPlayers();
//             PlayerContext playerContext = players[player];
//             return playerContext;
//         }
//     }
// }
using System.Collections.Generic;

namespace ShooterGame.Gameplay
{
    public static class PlayersUseCases
    {
        public static IPlayerContext GetPlayer(this IGameContext gameContext, TeamType teamType)
        {
            IDictionary<TeamType,IPlayerContext> players = gameContext.GetPlayers();
            return players[teamType];
        }
    }
}
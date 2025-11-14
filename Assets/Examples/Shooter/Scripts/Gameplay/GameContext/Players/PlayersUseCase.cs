using System.Collections.Generic;

namespace ShooterGame.Gameplay
{
    public static class PlayersUseCase
    {
        public static IPlayerContext GetPlayer(IGameContext context, IGameEntity character)
        {
            TeamType teamType = character.GetTeamType().Value;
            return GetPlayer(context, teamType);
        }

        public static IPlayerContext GetPlayer(IGameContext context, TeamType teamType)
        {
            IDictionary<TeamType, IPlayerContext> players = context.GetPlayers();
            return players[teamType];
        }
    }
}
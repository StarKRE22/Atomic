using System.Collections.Generic;

namespace ShooterGame.Gameplay
{
    public static class PlayersUseCase
    {
        public static IPlayerContext GetPlayerFor(IGameContext context, IWorldEntity character)
        {
            TeamType teamType = character.GetTeamType().Value;
            return GetPlayerFor(context, teamType);
        }

        public static IPlayerContext GetPlayerFor(IGameContext context, TeamType teamType)
        {
            IDictionary<TeamType, IPlayerContext> players = context.GetPlayers();
            return players[teamType];
        }
    }
}
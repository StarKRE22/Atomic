using System.Collections.Generic;

namespace SampleGame
{
    public static class PlayersUseCase
    {
        public static IPlayerContext GetPlayerFor(IGameContext context, IGameEntity character)
        {
            TeamType teamType = character.GetTeamType().Value;
            IDictionary<TeamType, IPlayerContext> players = context.GetPlayers();
            return players[teamType];
        }
    }
}
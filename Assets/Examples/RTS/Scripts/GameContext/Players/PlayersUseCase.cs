namespace RTSGame
{
    public static class PlayersUseCase
    {
        public static IPlayerContext GetPlayerFor(this IGameContext context, IGameEntity entity) => 
            GetPlayerFor(context, entity.GetTeam().Value);

        public static IPlayerContext GetPlayerFor(this IGameContext context, TeamType teamType) => 
            context.GetPlayers()[teamType];

    }
}
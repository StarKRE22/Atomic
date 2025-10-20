namespace RTSGame
{
    public static class PlayersUseCase
    {
        public static IPlayerContext GetPlayerFor(IGameContext context, IUnitEntity entity) => 
            GetPlayerFor(context, entity.GetTeam().Value);

        public static IPlayerContext GetPlayerFor(IGameContext context, TeamType teamType) => 
            context.GetPlayers()[teamType];
    }
}
namespace RTSGame
{
    public static class TeamUseCase
    {
        public static bool IsEnemyUnit(IPlayerContext context, IGameEntity target) => 
            target.HasUnitTag() && target.GetTeam().Value != context.GetTeam().Value;
    }
}
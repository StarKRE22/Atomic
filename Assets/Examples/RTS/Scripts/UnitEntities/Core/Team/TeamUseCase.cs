namespace RTSGame
{
    public static class TeamUseCase
    {
        public static bool IsFreeEnemyUnit(IPlayerContext context, IUnitEntity target) => 
            !target.HasTargetedTag() &&
            target.HasUnitTag() && 
            target.GetTeam().Value != context.GetTeam().Value;
    }
}
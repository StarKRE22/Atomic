namespace RTSGame
{
    public static class TeamUseCase
    {
        public static bool IsFreeEnemyUnit(IPlayerContext context, IGameEntity target) => 
            !target.HasTargetedTag() &&
            target.HasUnitTag() && 
            target.GetTeam().Value != context.GetTeam().Value;
        
        public static bool IsEnemy(this IGameEntity entity, IGameEntity target) => 
            !entity.Equals(target) &&
            target.HasUnitTag() && 
            target.GetTeam().Value != entity.GetTeam().Value;
    }
}
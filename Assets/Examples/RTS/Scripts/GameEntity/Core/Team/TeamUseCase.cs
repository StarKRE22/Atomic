namespace RTSGame
{
    public static class TeamUseCase
    {
        public static bool IsEnemyUnit(IGameEntity target, TeamType type) => 
            target.HasUnitTag() && target.GetTeam().Value != type;
    }
}
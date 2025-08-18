namespace RTSGame
{
    public static partial class TeamUseCase
    {
        public static bool IsEnemy(IGameEntity source, IGameEntity target)
        {
            return source.GetTeam().Value != target.GetTeam().Value;
        }
        
        public static bool IsEnemy(IGameEntity target, TeamType type)
        {
            return target.GetTeam().Value != type;
        }
    }
}
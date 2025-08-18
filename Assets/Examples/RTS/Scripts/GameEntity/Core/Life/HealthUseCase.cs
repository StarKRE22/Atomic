namespace RTSGame
{
    public static class HealthUseCase
    {
        public static bool IsAlive(IGameEntity entity) => entity.GetHealth().Exists();
    }
}
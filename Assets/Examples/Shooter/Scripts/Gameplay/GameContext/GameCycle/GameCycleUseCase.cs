namespace ShooterGame.Gameplay
{
    public static class GameCycleUseCase
    {
        public static bool IsPlaying(IGameContext context)
        {
            return context.GetGameTime().Value > 0;
        }
    }
}
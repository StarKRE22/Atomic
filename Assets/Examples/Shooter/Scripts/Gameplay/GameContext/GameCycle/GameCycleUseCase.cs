namespace ShooterGame.Gameplay
{
    public static class GameCycleUseCase
    {
        public static bool IsPlaying(IGameContext context) => 
            context.GetGameTime().Value > 0;
    }
}
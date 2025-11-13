using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public static class GameCycleUseCase
    {
        public static bool IsPlaying(IGameContext context)
        {
            IValue<float> variable = context.GetGameTime();
            return variable.Value > 0;
        }
    }
}
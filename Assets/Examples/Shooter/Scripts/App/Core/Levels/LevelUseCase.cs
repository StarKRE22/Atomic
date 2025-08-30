using Atomic.Elements;

namespace ShooterGame.App
{
    public static partial class LevelUseCase
    {
        public static void IncrementLevel(IAppContext context)
        {
            IVariable<int> currentLevel = context.GetCurrentLevel();
            IValue<int> maxLevel = context.GetMaxLevel();
            if (currentLevel.Value < maxLevel.Value) 
                currentLevel.Value++;
        }
    }
}
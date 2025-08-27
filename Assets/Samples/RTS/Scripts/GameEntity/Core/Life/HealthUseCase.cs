using System.Runtime.CompilerServices;

namespace RTSGame
{
    public static class HealthUseCase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(IGameEntity entity) => entity.GetHealth().Exists();
    }
}
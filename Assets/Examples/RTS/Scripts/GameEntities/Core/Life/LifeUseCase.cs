using System.Runtime.CompilerServices;

namespace RTSGame
{
    public static class LifeUseCase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(this IGameEntity entity) => entity.GetHealth().Exists();
    }
}
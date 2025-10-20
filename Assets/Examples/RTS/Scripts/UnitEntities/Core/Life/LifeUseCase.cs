using System.Runtime.CompilerServices;

namespace RTSGame
{
    public static class LifeUseCase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(IUnitEntity entity) => entity.GetHealth().Exists();
    }
}
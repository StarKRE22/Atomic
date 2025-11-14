using System.Runtime.CompilerServices;

namespace RTSGame
{
    public static class LifeUseCase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(IUnit entity) => entity.GetHealth().Exists();
    }
}
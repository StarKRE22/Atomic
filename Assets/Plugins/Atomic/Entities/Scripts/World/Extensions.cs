using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindTo<E>(this IEntityWorld<E> world, IEntity source) where E : IEntity
        {
            source.WhenInit(world.InitEntities);
            source.WhenEnable(world.Enable);
            source.WhenTick(world.Tick);
            source.WhenFixedTick(world.FixedTick);
            source.WhenLateTick(world.LateTick);
            source.WhenDisable(world.Disable);
            source.WhenDispose(world.DisposeEntities);
            source.WhenDispose(world.Dispose);
        }
    }
}
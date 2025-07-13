using System.Runtime.CompilerServices;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static class EntityAspectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyAspect(this IEntity obj, IEntityAspect entityAspect)
        {
            entityAspect.Apply(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DiscardAspect(this IEntity obj, IEntityAspect entityAspect)
        {
            entityAspect.Discard(obj);
        }
    }
}
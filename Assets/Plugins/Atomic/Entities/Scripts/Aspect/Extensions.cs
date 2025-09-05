using System.Runtime.CompilerServices;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Apply(this IEntity e, IEntityAspect entityAspect) => entityAspect.Apply(e);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Apply<E>(this E e, IEntityAspect<E> entityAspect) where E : IEntity => entityAspect.Apply(e);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discard(this IEntity e, IEntityAspect entityAspect) => entityAspect.Discard(e);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discard(this E e, IEntityAspect<> entityAspect) => entityAspect.Discard(e);
    }
}
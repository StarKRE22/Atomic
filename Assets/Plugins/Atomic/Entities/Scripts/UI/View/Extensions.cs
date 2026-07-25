using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<E, T>(this EntityView<E> view, ValueKey<E, T> key) where E : class, IEntity =>
            view.Entity.GetValue<T>(key.Id);
    }
}
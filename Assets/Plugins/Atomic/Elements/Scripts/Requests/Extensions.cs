using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired(this IRequest request) => request.Required;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T>(this IRequest<T> request) => request.Required;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2>(this IRequest<T1, T2> request) => request.Required;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2, T3>(this IRequest<T1, T2, T3> request) => request.Required;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2, T3, T4>(this IRequest<T1, T2, T3, T4> request) => request.Required;
    }
}
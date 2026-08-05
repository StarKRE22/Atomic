using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with requests.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether the request is marked as required.
        /// </summary>
        /// <param name="request">The request to inspect.</param>
        /// <returns>
        /// <see langword="true"/> if the request is required; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired(this IRequest request) => request.Required;

        /// <summary>
        /// Determines whether the request is marked as required.
        /// </summary>
        /// <typeparam name="T">The request argument type.</typeparam>
        /// <param name="request">The request to inspect.</param>
        /// <returns>
        /// <see langword="true"/> if the request is required; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T>(this IRequest<T> request) => request.Required;

        /// <summary>
        /// Determines whether the request is marked as required.
        /// </summary>
        /// <typeparam name="T1">The first request argument type.</typeparam>
        /// <typeparam name="T2">The second request argument type.</typeparam>
        /// <param name="request">The request to inspect.</param>
        /// <returns>
        /// <see langword="true"/> if the request is required; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2>(this IRequest<T1, T2> request) => request.Required;

        /// <summary>
        /// Determines whether the request is marked as required.
        /// </summary>
        /// <typeparam name="T1">The first request argument type.</typeparam>
        /// <typeparam name="T2">The second request argument type.</typeparam>
        /// <typeparam name="T3">The third request argument type.</typeparam>
        /// <param name="request">The request to inspect.</param>
        /// <returns>
        /// <see langword="true"/> if the request is required; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2, T3>(this IRequest<T1, T2, T3> request) => request.Required;

        /// <summary>
        /// Determines whether the request is marked as required.
        /// </summary>
        /// <typeparam name="T1">The first request argument type.</typeparam>
        /// <typeparam name="T2">The second request argument type.</typeparam>
        /// <typeparam name="T3">The third request argument type.</typeparam>
        /// <typeparam name="T4">The fourth request argument type.</typeparam>
        /// <param name="request">The request to inspect.</param>
        /// <returns>
        /// <see langword="true"/> if the request is required; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRequired<T1, T2, T3, T4>(this IRequest<T1, T2, T3, T4> request) => request.Required;
    }
}

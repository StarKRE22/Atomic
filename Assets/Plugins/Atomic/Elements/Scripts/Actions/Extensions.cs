using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Extension methods for working with <see cref="IAction"/> collections.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Invokes all actions in the given enumerable sequence.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="actions">A sequence of actions to invoke.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAll(this IEnumerable<IAction> actions)
        {
            if (actions != null)
                foreach (IAction action in actions)
                    action?.Invoke();
        }
    }
}
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAll(this IEnumerable<IAction> actions)
        {
            if (actions != null)
            {
                foreach (IAction action in actions)
                {
                    action?.Invoke();
                }    
            }
        } 
    }
}
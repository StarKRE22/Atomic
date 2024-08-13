using System;

namespace Atomic.Entities
{
    internal sealed class DelegateUtils
    {
        internal static void Unsubscribe(ref Action action)
        {
            if (action == null)
            {
                return;
            }

            Delegate[] delegates = action.GetInvocationList();
            foreach (var del in delegates)
            {
                action -= (Action) del;
            }

            action = null;
        }
        
        internal static void Unsubscribe<T>(ref Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            Delegate[] delegates = action.GetInvocationList();
            foreach (var del in delegates)
            {
                action -= (Action<T>) del;
            }

            action = null;
        }
    }
}
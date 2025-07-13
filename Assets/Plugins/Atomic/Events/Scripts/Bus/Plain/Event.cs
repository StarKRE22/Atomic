using System;

namespace Atomic.Events
{
    internal interface IEvent : IDisposable
    {
    }

    internal sealed class Event : IEvent
    {
        public Action Delegate;

        public void Dispose()
        {
            if (Delegate == null)
                return;

            Delegate[] delegates = Delegate.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                Delegate -= (Action) delegates[i];

            Delegate = null;
        }
    }

    internal sealed class Event<T> : IEvent
    {
        public Action<T> Delegate;

        public void Dispose()
        {
            if (Delegate == null)
                return;

            Delegate[] delegates = Delegate.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                Delegate -= (Action<T>) delegates[i];

            Delegate = null;
        }
    }

    internal sealed class Event<T1, T2> : IEvent
    {
        public Action<T1, T2> Delegate;

        public void Dispose()
        {
            if (Delegate == null)
                return;

            Delegate[] delegates = Delegate.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                Delegate -= (Action<T1, T2>) delegates[i];

            Delegate = null;
        }
    }

    internal sealed class Event<T1, T2, T3> : IEvent
    {
        public Action<T1, T2, T3> Delegate;

        public void Dispose()
        {
            if (Delegate == null)
                return;

            Delegate[] delegates = Delegate.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                Delegate -= (Action<T1, T2, T3>) delegates[i];

            Delegate = null;
        }
    }
}
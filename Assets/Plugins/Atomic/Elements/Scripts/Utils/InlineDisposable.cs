using System;

namespace Atomic.Elements
{
    public class InlineDisposable : IDisposable
    {
        private readonly Action _dispose;

        public InlineDisposable(Action dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        public void Dispose() => _dispose.Invoke();
    }
}
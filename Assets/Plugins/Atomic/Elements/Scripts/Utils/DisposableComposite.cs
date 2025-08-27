using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite disposable container that manages multiple <see cref="IDisposable"/> objects.
    /// Disposing the composite disposes all contained objects.
    /// </summary>
    public class DisposableComposite : IDisposable
    {
        /// <summary>
        /// Gets the count of disposables currently in the composite.
        /// </summary>
        public int Count => _items.Count;

        private readonly List<IDisposable> _items;
        private bool _isDisposed;

        public DisposableComposite(params IDisposable[] disposables)
        {
            _items = new List<IDisposable>(disposables);
        }

        /// <summary>
        /// Adds a disposable resource to the composite.
        /// </summary>
        /// <param name="disposable">The <see cref="IDisposable"/> to add.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the composite has already been disposed.</exception>
        public DisposableComposite Add(IDisposable disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));

            if (_isDisposed)
            {
                disposable.Dispose();
                throw new ObjectDisposedException(nameof(DisposableComposite),
                    "Cannot add disposables to a disposed composite. The disposable has been immediately disposed.");
            }

            _items.Add(disposable);
            return this;
        }

        /// <summary>
        /// Disposes all contained <see cref="IDisposable"/> objects and clears the composite.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            List<Exception> exceptions = null;

            foreach (var item in _items)
            {
                try
                {
                    item.Dispose();
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(ex);
                }
            }

            _items.Clear();

            if (exceptions != null)
                throw new AggregateException("One or more disposables threw an exception during disposal.", exceptions);
        }
    }
}
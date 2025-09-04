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

        /// <summary>
        /// Initializes a new instance of <see cref="DisposableComposite"/> with a collection of disposables.
        /// </summary>
        /// <param name="disposables">The initial disposables to add.</param>
        public DisposableComposite(IEnumerable<IDisposable> disposables) => _items = new List<IDisposable>(disposables);

        /// <summary>
        /// Initializes a new instance of <see cref="DisposableComposite"/> with a params array of disposables.
        /// </summary>
        /// <param name="disposables">The initial disposables to add.</param>
        public DisposableComposite(params IDisposable[] disposables) => _items = new List<IDisposable>(disposables);

        /// <summary>
        /// Adds a new <see cref="IDisposable"/> to the composite.
        /// </summary>
        /// <param name="disposable">The disposable to add.</param>
        /// <returns>The current composite instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="disposable"/> is null.</exception>
        public DisposableComposite Add(IDisposable disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));

            _items.Add(disposable);
            return this;
        }

        /// <summary>
        /// Disposes all contained <see cref="IDisposable"/> objects and clears the composite.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0, count = _items.Count; i < count; i++)
                _items[i].Dispose();

            _items.Clear();
        }
    }
}
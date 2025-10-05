using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite disposable container that manages multiple <see cref="T"/> objects.
    /// Disposing the composite disposes all contained objects.
    /// </summary>
    public class DisposableComposite<T> : IDisposable where T : IDisposable
    {
        /// <summary>
        /// Gets the count of disposables currently in the composite.
        /// </summary>
        public int Count => _items.Count;

        private readonly List<T> _items;

        /// <summary>
        /// Initializes a new instance of <see cref="DisposableComposite{T}"/> with a collection of disposables.
        /// </summary>
        /// <param name="disposables">The initial disposables to add.</param>
        public DisposableComposite(IEnumerable<T> disposables) => _items = new List<T>(disposables);

        /// <summary>
        /// Initializes a new instance of <see cref="DisposableComposite{T}"/> with a params array of disposables.
        /// </summary>
        /// <param name="disposables">The initial disposables to add.</param>
        public DisposableComposite(params T[] disposables) => _items = new List<T>(disposables);

        /// <summary>
        /// Adds a new <see cref="T"/> to the composite.
        /// </summary>
        /// <param name="disposable">The disposable to add.</param>
        /// <returns>The current composite instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="disposable"/> is null.</exception>
        public DisposableComposite<T> Add(T disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));

            _items.Add(disposable);
            return this;
        }

        /// <summary>
        /// Disposes all contained <see cref="T"/> objects and clears the composite.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0, count = _items.Count; i < count; i++)
                _items[i].Dispose();

            _items.Clear();
        }
    }
}
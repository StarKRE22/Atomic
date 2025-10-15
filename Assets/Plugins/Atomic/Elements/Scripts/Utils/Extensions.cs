using System;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        /// <summary>
        /// Adds the current <see cref="IDisposable"/> instance to a <see cref="DisposableComposite"/>.
        /// This allows chaining disposables directly into a composite for automatic disposal.
        /// </summary>
        /// <param name="it">The <see cref="IDisposable"/> instance to add.</param>
        /// <param name="composite">The <see cref="DisposableComposite"/> to which the disposable will be added.</param>
        public static void AddTo(this IDisposable it, DisposableComposite composite) => 
            composite.Add(it);
        
        /// <summary>
        /// Adds the current <see cref="IDisposable"/> instance to a <see cref="DisposableComposite"/>.
        /// This allows chaining disposables directly into a composite for automatic disposal.
        /// </summary>
        /// <param name="it">The <see cref="IDisposable"/> instance to add.</param>
        /// <param name="composite">The <see cref="DisposableComposite"/> to which the disposable will be added.</param>
        public static void AddTo<T>(this T it, DisposableComposite<T> composite) where T : IDisposable =>
            composite.Add(it);
    }
}
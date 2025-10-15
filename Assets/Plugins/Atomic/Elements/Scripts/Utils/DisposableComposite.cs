using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite disposable container that manages multiple <see cref="IDisposable"/> objects.
    /// Disposing the composite disposes all contained objects.
    /// </summary>
    public class DisposableComposite : DisposableComposite<IDisposable>
    {
    }
}
using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that tracks progress (0–1) and notifies listeners.
    /// </summary>
    public interface IProgressSource
    {
        /// <summary>Raised when the progress changes.</summary>
        event Action<float> OnProgressChanged;

        /// <summary>Gets the current progress.</summary>
        /// <returns>Normalized progress (0–1).</returns>
        float GetProgress();

        /// <summary>Sets the current progress.</summary>
        /// <param name="progress">Progress value (0–1).</param>
        void SetProgress(float progress);
    }
}
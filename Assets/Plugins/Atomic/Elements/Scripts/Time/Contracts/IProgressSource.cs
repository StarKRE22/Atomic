using System;

namespace Atomic.Elements
{
    public interface IProgressSource
    {
        /// <summary>
        /// Invoked when the progress (0 to 1) changes.
        /// </summary>
        event Action<float> OnProgressChanged;
        
        /// <summary>
        /// Gets the progress of the cooldown (from 0 to 1).
        /// </summary>
        float GetProgress();

        /// <summary>
        /// Sets the progress (from 0 to 1), updating the remaining time accordingly.
        /// </summary>
        /// <param name="progress">The new progress value (0–1).</param>
        void SetProgress(float progress);
    }
}
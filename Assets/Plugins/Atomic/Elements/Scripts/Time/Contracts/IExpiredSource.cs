using System;

namespace Atomic.Elements
{
    public interface IExpiredSource
    {
        /// <summary>
        /// Invoked when the cooldown has expired (time reaches zero).
        /// </summary>
        event Action OnExpired;
        
        /// <summary>
        /// Returns whether the cooldown has expired (i.e., current time is zero or less).
        /// </summary>
        bool IsExpired();
    }
}
using System;

namespace Atomic.Entities
{
    public interface IActivatable
    {
        event Action OnEnabled;

        event Action OnDisabled;

        bool Enabled { get; }

        void Enable();

        void Disable();
    }
}
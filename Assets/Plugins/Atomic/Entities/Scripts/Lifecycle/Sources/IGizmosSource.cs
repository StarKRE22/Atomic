#if UNITY_EDITOR
using System;

namespace Atomic.Entities
{
    public interface IGizmosSource
    {
        event Action OnGizmosDraw;
    }
}
#endif
#if UNITY_EDITOR
using System.Collections.Generic;

namespace Atomic.Events
{
    public interface IEventAPIConfig
    {
        string Namespace { get; }
        string ClassName { get; }
        string Directory { get; }
        
        string EventBusType { get; }
        bool AggressiveInlining { get; }

        IReadOnlyCollection<string> GetImports();
        IReadOnlyCollection<EventDefinition> GetEvents();
    }
}
#endif
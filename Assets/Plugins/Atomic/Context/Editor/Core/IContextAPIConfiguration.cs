#if UNITY_EDITOR
using System.Collections.Generic;

namespace Atomic.Contexts
{
    public interface IContextAPIConfiguration
    {
        string Namespace { get; }
        string ClassName { get; }
        string Directory { get; }
        
        string ContextType { get; }
        bool AggressiveInlining { get; }

        IReadOnlyCollection<string> GetImports();
        IDictionary<string, string> GetValues();
    }
}

#endif
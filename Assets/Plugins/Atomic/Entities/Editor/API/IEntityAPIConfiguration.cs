#if UNITY_EDITOR
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntityAPIConfiguration
    {
        string Namespace { get; }
        string ClassName { get; }
        string Directory { get; }
        
        string EntityType { get; }
        bool AggressiveInlining { get; }
        bool UnsafeAccess { get; }

        IReadOnlyCollection<string> GetImports();
        IReadOnlyCollection<string> GetTags();
        IDictionary<string, string> GetValues();
    }
}
#endif
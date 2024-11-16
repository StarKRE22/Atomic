#if UNITY_EDITOR
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntityAPIConfiguration
    {
        string Namespace { get; }
        string ClassName { get; }
        string Directory { get; }

        IEnumerable<string> GetImports();
        IEnumerable<string> GetTags();
        IDictionary<string, string> GetValues();
    }
}
#endif
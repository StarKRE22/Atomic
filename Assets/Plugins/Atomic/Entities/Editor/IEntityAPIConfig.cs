#if UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntityAPIConfig
    {
        string Namespace { get; }
        string ClassName { get; }
        string DirectoryPath { get; }

        IEnumerable<string> GetImports();

        IEnumerable<string> GetTags();
        IDictionary<string, Type> GetValues();
    }
}
#endif
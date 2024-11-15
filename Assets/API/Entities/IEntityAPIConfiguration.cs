using System;
using System.Collections.Generic;

namespace Atomic.API
{
    public interface IEntityAPIConfiguration
    {
        string Namespace { get; }
        string ClassName { get; }
        string DirectoryPath { get; }

        IEnumerable<string> GetImports();

        IEnumerable<string> GetTags();
        IDictionary<string, Type> GetValues();
    }
}
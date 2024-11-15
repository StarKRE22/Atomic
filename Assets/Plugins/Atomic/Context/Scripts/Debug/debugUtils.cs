using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace Atomic.Contexts
{
    public static class debugUtils
    {
        public static Func<int, string> ValueNameFormatter;

        public static string ConvertToName(int id)
        {
            return ValueNameFormatter?.Invoke(id) ?? id.ToString();
        }
    }
}
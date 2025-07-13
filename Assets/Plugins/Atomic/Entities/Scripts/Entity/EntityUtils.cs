using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(in string name) => new PropertyName(name).GetHashCode();

        ///For debugging purposes only. Returns the string value representing the string in the Editor.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(in int id) => IdToFullName(id).Split(':')[0];

        ///For debugging purposes only. Returns the string value representing the string in the Editor.
        public static string IdToFullName(in int id) => new PropertyName(id).ToString();
    }
}
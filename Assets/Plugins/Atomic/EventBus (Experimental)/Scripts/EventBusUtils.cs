using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Events
{
    public static class EventBusUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(string name) => new PropertyName(name).GetHashCode();

        ///For debugging purposes only. Returns the string value representing the string in the Editor.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(int id) => IdToFullName(id).Split(':')[0];

        ///For debugging purposes only. Returns the string value representing the string in the Editor.
        public static string IdToFullName(int id) => new PropertyName(id).ToString();
    }
}
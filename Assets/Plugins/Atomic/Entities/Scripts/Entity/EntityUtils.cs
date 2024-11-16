using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityUtils
    {
        private static readonly Func<PropertyName, string> _propertyNameToName;

        static EntityUtils()
        {
            _propertyNameToName = (Func<PropertyName, string>) Type
                .GetType("UnityEngine.PropertyNameUtils")!
                .GetMethod("StringFromPropertyName", BindingFlags.Static | BindingFlags.NonPublic)!
                .CreateDelegate(typeof(Func<PropertyName, string>));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(string name)
        {
            return new PropertyName(name).GetHashCode();
        }

        ///For debugging purposes only. Returns the string value representing the string in the Editor.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToString(int id)
        {
            return new PropertyName(id).ToString();
        }

        public static string IdToName(int id)
        {
            return _propertyNameToName.Invoke(id);
        }
    }
}
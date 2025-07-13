using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class UnsafeTests
    {
        [Test]
        public void UnsafeTest()
        {
            ReactiveVariable<string> name = new ReactiveVariable<string>("Vasya");
            object obj = name;
            ISetter<string> result = UnsafeUtility.As<object, ISetter<string>>(ref obj);
            result.Value = "ffff";
            
            Debug.Log($"RESULT {name.Value}");
        }
        
    }
}
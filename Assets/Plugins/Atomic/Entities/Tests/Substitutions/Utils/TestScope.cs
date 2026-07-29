// using System;
// using System.Collections.Generic;
//
// namespace Atomic.Entities
// {
//     public abstract class TestScope<T> : IDisposable where T : class
//     {
//         private readonly List<T> _objects = new();
//
//         
//         public T Create()
//         {
//             T obj = this.CreateInternal();
//             _objects.Add(obj);
//             return obj;
//         }
//         
//         protected abstract T CreateInternal();
//         
//         public void Dispose()
//         {
//             for (int i = 0, count = _objects.Count; i < count; i++)
//                 this.DisposeInternal(_objects[i]);
//
//             _objects.Clear();
//         }
//
//         protected abstract void DisposeInternal(T obj);
//     }
// }
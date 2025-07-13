// using System.Collections.Generic;
// using NUnit.Framework;
// using Unity.Collections.LowLevel.Unsafe;
// using Unity.PerformanceTesting;
// using UnityEngine;
//
// namespace Atomic.Entities
// {
//     public sealed class Performance_GetValue
//     {
//         [Test, Performance]
//         public void GetValue_Array()
//         {
//             object[] array = new object[1000];
//
//             for (int i = 0; i < 1000; i++)
//                 array[i] = new string("Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         object vasya = array[i];
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//         
//         [Test, Performance]
//         public void GetValue_ArrayCast()
//         {
//             object[] array = new object[1000];
//
//             for (int i = 0; i < 1000; i++)
//                 array[i] = new string("Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         string vasya = (string) array[i];
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         [Test, Performance]
//         public void GetValue_ArrayUnsafeCast()
//         {
//             object[] array = new object[1000];
//
//             for (int i = 0; i < 1000; i++)
//                 array[i] = new string("Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         object value = array[i];
//                         ref string vasya = ref UnsafeUtility.As<object, string>(ref value);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         [Test, Performance]
//         public void GetValue_Dictionary()
//         {
//             Dictionary<int, object> dictionary = new Dictionary<int, object>();
//             for (int i = 0; i < 1000; i++)
//                 dictionary.Add(i, "Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         object vasya = dictionary[i];
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         [Test, Performance]
//         public void GetValue_DictionaryCast()
//         {
//             Dictionary<int, object> dictionary = new Dictionary<int, object>();
//             for (int i = 0; i < 1000; i++)
//                 dictionary.Add(i, "Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         string vasya = (string) dictionary[i];
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//
//         [Test, Performance]
//         public void GetValue_DictionaryUnsafeCast()
//         {
//             Dictionary<int, object> dictionary = new Dictionary<int, object>();
//             for (int i = 0; i < 1000; i++)
//                 dictionary.Add(i, "Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         object value = dictionary[i];
//                         ref string vasya = ref UnsafeUtility.As<object, string>(ref value);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         [Test, Performance]
//         public void GetReferenceValue_Entity()
//         {
//             var entity = new Entity();
//             for (int i = 0; i < 1000; i++)
//                 entity.AddValue(i, "Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         string vasya = entity.GetValue<string>(i);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//         
//         [Test, Performance]
//         public void GetPrimitiveValue_Entity()
//         {
//             var entity = new Entity();
//             for (int i = 0; i < 1000; i++)
//                 entity.AddValue(i, 777);
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         int vasya = entity.GetValue<int>(i);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//
//         [Test, Performance]
//         public void GetReferenceValueUnsafe_Entity()
//         {
//             var entity = new Entity();
//             for (int i = 0; i < 1000; i++)
//                 entity.AddValue(i, "Vasya");
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         string vasya = entity.GetValueUnsafe<string>(i);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//         
//         [Test, Performance]
//         public void GetPrimitiveValueUnsafe_Entity()
//         {
//             var entity = new Entity();
//             for (int i = 0; i < 1000; i++)
//                 entity.AddValue(i, 777);
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         int vasya = entity.GetValueUnsafe<int>(i);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         [Test, Performance]
//         public void GetComponent_GameObject()
//         {
//             var gameObjects = new List<GameObject>();
//             for (int i = 0; i < 1000; i++)
//                 gameObjects.Add(new GameObject($"{i}"));
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         Transform transform = gameObjects[i].GetComponent<Transform>();
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(30)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//     }
// }
// using System.Collections.Generic;
// using NUnit.Framework;
// using Unity.PerformanceTesting;
// using UnityEngine;
//
// namespace Atomic.Entities
// {
//     [TestFixture]
//     public sealed class MeasureTests
//     {
//         [Test, Performance]
//         public void GetValuePerformanceTest()
//         {
//             Entity entity = new Entity("AAA");
//             Dictionary<int, object> dict = new Dictionary<int, object>();
//             object[] array = new object[1000];
//
//             for (int i = 0; i < 1000; i++)
//             {
//                 entity.AddValue(i, new string("FFF"));
//                 dict.Add(i, new string("FFF"));
//                 array[i] = new string("FFF");
//             }
//
//             Measure.Method(() =>
//                 {
//                     IReadOnlyDictionary<int,object> d = entity.Values;
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         // dict.TryGetValue(i, out var obj);
//                         // string res =  obj as string;
//
//                         // string o = dict[i] as string;
//
//                         // string o = (string) d[i];
//
//                         // string o = (string) dict[i];
//                         // string fff = (string) array[i];
//                         string fff = entity.GetValue<string>(i);
//                     }
//                 })
//                 .WarmupCount(10)
//                 .MeasurementCount(25)
//                 // .SampleGroup(new SampleGroup("Time", SampleUnit.Nanosecond))
//                 .Run();
//         }
//
//         private sealed class ReactiveInt
//         {
//             public int value;
//
//             public ReactiveInt(int value)
//             {
//                 this.value = value;
//             }
//         }
//
//         [Test, Performance]
//         public void UpdatePerformanceTest2()
//         {
//             List<Entity> entityList = new List<Entity>();
//             for (int i = 0; i < 1000; i++)
//             {
//                 var entity = new Entity($"{i}");
//
//                 for (int j = 0; j < 50; j++)
//                 {
//                     entity.AddBehaviour<JobStub>();
//                 }
//
//                 for (int j = 0; j < 50; j++)
//                 {
//                     entity.AddValue(j, new ReactiveInt(Random.Range(0, 100)));
//                 }
//
//                 entity.Enable();
//                 entityList.Add(entity);
//             }
//
//
//             Measure.Method(() =>
//                 {
//                     for (int i = 0; i < 1000; i++)
//                     {
//                         Entity entity = entityList[i];
//                         entity.OnUpdate(0.02f);
//                     }
//                 })
//                 .WarmupCount(5)
//                 .SampleGroup(new SampleGroup("Time", SampleUnit.Second))
//                 .MeasurementCount(10)
//                 .Run();
//         }
//
//         private sealed class JobStub : IEntityUpdate
//         {
//             //Делаем какую-то работу!
//             public void OnUpdate(IEntity entity, float deltaTime)
//             {
//                 int sum = 0;
//                 for (int i = 0; i < 5; i++)
//                 {
//                     ReactiveInt reactiveInt = entity.GetValue<ReactiveInt>(Random.Range(0, 50));
//                     sum += reactiveInt.value;
//                 }
//
//                 ReactiveInt res = entity.GetValue<ReactiveInt>(Random.Range(0, 50));
//                 res.value = sum / 5;
//             }
//         }
//     }
// }
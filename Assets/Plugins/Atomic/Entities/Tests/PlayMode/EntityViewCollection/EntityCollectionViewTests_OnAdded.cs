// using System.Collections;
// using NUnit.Framework;
// using UnityEngine.TestTools;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         
//         [UnityTest]
//         public IEnumerator OnAdded_IsRaised_WhenEntityAdded()
//         {
//             var entity = new Entity("Player");
//
//             IEntity receivedEntity = null;
//             EntityView receivedView = null;
//
//             world.OnAdded += (e, v) =>
//             {
//                 receivedEntity = e;
//                 receivedView = v;
//             };
//
//             world.AddView(entity);
//             yield return null;
//
//             Assert.AreEqual(entity, receivedEntity);
//             Assert.NotNull(receivedView);
//             Assert.AreEqual("Player", receivedView.Entity.Name);
//         }
//
//         [UnityTest]
//         public IEnumerator OnAdded_NotRaised_WhenEntityAlreadyExists()
//         {
//             var entity = new Entity("Enemy");
//             int callCount = 0;
//
//             world.OnAdded += (_, _) => callCount++;
//
//             world.AddView(entity);
//             world.AddView(entity); // второй раз та же сущность
//             yield return null;
//
//             Assert.AreEqual(1, callCount, "OnAdded должно быть вызвано только один раз");
//         }
//
//         [UnityTest]
//         public IEnumerator OnAdded_IsRaised_ForEachEntity_WhenShowCalled()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//             var source = new EntityCollection(entityA, entityB);
//
//             int callCount = 0;
//             world.OnAdded += (_, _) => callCount++;
//
//             world.Show(source);
//             yield return null;
//
//             Assert.AreEqual(2, callCount, "Оба объекта должны вызвать OnAdded");
//         }
//
//     }
// }

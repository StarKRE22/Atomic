// using NUnit.Framework;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void OnRemoved_IsRaised_WhenEntityRemoved()
//         {
//             var entity = new Entity("Player");
//             IEntity removedEntity = null;
//             EntityView removedView = null;
//
//             world.AddView(entity);
//             world.OnRemoved += (e, v) =>
//             {
//                 removedEntity = e;
//                 removedView = v;
//             };
//
//             world.RemoveView(entity);
//
//             Assert.AreEqual(entity, removedEntity);
//             Assert.NotNull(removedView);
//             Assert.AreEqual("Player(Clone)", removedView.name);
//         }
//
//         [Test]
//         public void OnRemoved_NotRaised_WhenEntityNotInCollection()
//         {
//             var entity = new Entity("Enemy");
//             int callCount = 0;
//
//             world.OnRemoved += (_, _) => callCount++;
//
//             world.RemoveView(entity);
//
//             Assert.AreEqual(0, callCount, "OnRemoved должно вызываться только при реальном удалении");
//         }
//
//         [Test]
//         public void OnRemoved_CalledOnce_WhenEntityRemovedTwice()
//         {
//             var entity = new Entity("Enemy");
//             int callCount = 0;
//
//             world.AddView(entity);
//             world.OnRemoved += (_, _) => callCount++;
//
//             world.RemoveView(entity);
//             world.RemoveView(entity); // повторное удаление
//
//             Assert.AreEqual(1, callCount, "OnRemoved должно вызваться только один раз");
//         }
//     }
// }
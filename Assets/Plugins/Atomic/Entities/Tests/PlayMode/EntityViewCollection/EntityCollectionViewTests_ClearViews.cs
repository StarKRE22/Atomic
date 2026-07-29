// using NUnit.Framework;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void ClearViews_DoesNothing_WhenCollectionIsEmpty()
//         {
//             Assert.AreEqual(0, world.Count, "Count должен быть 0 перед очисткой");
//             
//             Assert.DoesNotThrow(() => world.ClearPools(), "ClearViews не должно падать на пустой коллекции");
//             
//             Assert.AreEqual(0, world.Count, "Count должен остаться 0 после ClearViews");
//         }
//
//         [Test]
//         public void ClearViews_RemovesAllViews_AndRaisesOnRemoved()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//
//             world.AddView(entityA);
//             world.AddView(entityB);
//
//             int removedCount = 0;
//             world.OnRemoved += (_, _) => removedCount++;
//
//             world.ClearPools();
//
//             Assert.AreEqual(0, world.Count, "После ClearViews коллекция должна быть пустой");
//             Assert.AreEqual(2, removedCount, "OnRemoved должно быть вызвано для каждой сущности");
//         }
//
//         [Test]
//         public void ClearViews_AllowsNewViewsAfterwards()
//         {
//             var entity = new Entity("Player");
//             world.AddView(entity);
//
//             world.ClearPools();
//
//             var newEntity = new Entity("Enemy");
//             world.AddView(newEntity);
//
//             Assert.AreEqual(1, world.Count, "После ClearViews можно добавлять новые сущности");
//             Assert.NotNull(world.GetView(newEntity), "Новая сущность должна корректно добавляться");
//         }
//     }
// }
// using NUnit.Framework;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void Count_Increases_WhenEntityAdded()
//         {
//             var entity = new Entity("Player");
//
//             Assert.AreEqual(0, world.Count, "Изначально коллекция должна быть пустой");
//
//             world.AddView(entity);
//
//             Assert.AreEqual(1, world.Count, "После добавления одной сущности Count должен быть равен 1");
//         }
//
//         [Test]
//         public void Count_Decreases_WhenEntityRemoved()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//
//             world.AddView(entityA);
//             world.AddView(entityB);
//
//             Assert.AreEqual(2, world.Count, "После добавления двух сущностей Count должен быть равен 2");
//
//             world.RemoveView(entityA);
//
//             Assert.AreEqual(1, world.Count, "После удаления одной сущности Count должен уменьшиться до 1");
//         }
//
//         [Test]
//         public void Count_ReturnsZero_WhenCleared()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//
//             world.AddView(entityA);
//             world.AddView(entityB);
//
//             Assert.AreEqual(2, world.Count, "Count должен быть равен 2 перед очисткой");
//
//             world.ClearPools();
//
//             Assert.AreEqual(0, world.Count, "После ClearViews Count должен быть равен 0");
//         }
//     }
// }
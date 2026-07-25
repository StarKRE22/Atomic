// using NUnit.Framework;
// using System;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void Hide_DoesNothing_WhenNoSource()
//         {
//             Assert.DoesNotThrow(() => world.Hide(), "Hide не должен падать, если источник не задан");
//             Assert.AreEqual(0, world.Count, "Count должен оставаться 0");
//             Assert.IsFalse(world.IsShown, "IsVisible должен быть false");
//         }
//
//         [Test]
//         public void Hide_UnsubscribesFromSourceEvents_AndClearsViews()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//             var source = new EntityCollection(entityA, entityB);
//
//             world.Show(source);
//
//             Assert.AreEqual(2, world.Count, "Перед Hide коллекция должна содержать 2 вьюшки");
//
//             world.Hide();
//
//             // Count обнуляется
//             Assert.AreEqual(0, world.Count, "После Hide все views должны быть удалены");
//
//             // IsVisible должен стать false
//             Assert.IsFalse(world.IsShown, "После Hide коллекция не видима");
//
//             // Проверяем, что новые сущности не добавляются автоматически
//             var newEntity = new Entity("NPC");
//             source.Add(newEntity);
//
//             Assert.AreEqual(0, world.Count, "После Hide новые сущности в коллекцию не добавляются");
//         }
//     }
// }
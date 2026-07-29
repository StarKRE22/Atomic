// using System;
// using System.Collections.Generic;
// using NUnit.Framework;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void Show_Throws_WhenSourceIsNull()
//         {
//             Assert.Throws<ArgumentNullException>(() => world.Show(null));
//         }
//
//         [Test]
//         public void Show_AddsEntities_FromSource()
//         {
//             var entityA = new Entity("Player");
//             var entityB = new Entity("Enemy");
//             var source = new EntityCollection(entityA, entityB);
//
//             world.Show(source);
//
//             Assert.AreEqual(2, world.Count, "Все сущности из источника должны быть добавлены");
//             Assert.NotNull(world.GetView(entityA));
//             Assert.NotNull(world.GetView(entityB));
//         }
//
//         [Test]
//         public void Show_SubscribesToSourceEvents()
//         {
//             var entity = new Entity("Player");
//             var source = new EntityCollection(entity);
//
//             world.Show(source);
//
//             var newEntity = new Entity("Enemy");
//             source.Add(newEntity); // должно подтянуть вьюшку автоматически
//
//             Assert.AreEqual(2, world.Count, "После добавления в источник коллекция должна обновиться");
//             Assert.NotNull(world.GetView(newEntity));
//         }
//
//         [Test]
//         public void Show_ReplacesPreviousSource()
//         {
//             var entityA = new Entity("Player");
//             var source1 = new EntityCollection(entityA);
//
//             var entityB = new Entity("Enemy");
//             var source2 = new EntityCollection(entityB);
//
//             world.Show(source1);
//             world.Show(source2); // должно вызвать Hide() и очистить старые вьюшки
//
//             Assert.AreEqual(1, world.Count, "После смены источника должны остаться только сущности из нового источника");
//             Assert.IsTrue(world.IsShown);
//             Assert.NotNull(world.GetView(entityB));
//             Assert.Throws<KeyNotFoundException>(() => world.GetView(entityA), "Старые сущности должны быть удалены");
//         }
//     }
// }

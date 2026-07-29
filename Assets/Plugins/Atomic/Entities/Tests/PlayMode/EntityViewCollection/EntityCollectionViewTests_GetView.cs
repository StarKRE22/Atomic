// using NUnit.Framework;
// using System.Collections.Generic;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void GetView_ReturnsView_ForExistingEntity()
//         {
//             var entity = new Entity("Player");
//
//             world.AddView(entity);
//
//             var view = world.GetView(entity);
//
//             Assert.NotNull(view, "GetView должен вернуть вьюшку для существующей сущности");
//             Assert.AreEqual(entity, view.Entity, "Вьюшка должна быть связана с правильной сущностью");
//         }
//
//         [Test]
//         public void GetView_ThrowsKeyNotFoundException_ForNonExistingEntity()
//         {
//             var entity = new Entity("Enemy");
//
//             Assert.Throws<KeyNotFoundException>(
//                 () => world.GetView(entity),
//                 "Если сущности нет в коллекции, должно выбрасываться KeyNotFoundException"
//             );
//         }
//     }
// }
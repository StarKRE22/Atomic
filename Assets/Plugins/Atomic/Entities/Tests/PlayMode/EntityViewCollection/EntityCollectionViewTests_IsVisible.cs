// using NUnit.Framework;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         [Test]
//         public void IsVisible_False_ByDefault()
//         {
//             Assert.IsFalse(world.IsShown, "По умолчанию коллекция не должна быть видимой");
//         }
//
//         [Test]
//         public void IsVisible_True_AfterShow()
//         {
//             var entity = new Entity("Player");
//             var source = new EntityCollection(entity);
//
//             world.Show(source);
//
//             Assert.IsTrue(world.IsShown, "После вызова Show коллекция должна быть видимой");
//         }
//
//         [Test]
//         public void IsVisible_False_AfterHide()
//         {
//             var entity = new Entity("Player");
//             var source = new EntityCollection(entity);
//
//             world.Show(source);
//             world.Hide();
//
//             Assert.IsFalse(world.IsShown, "После вызова Hide коллекция должна стать невидимой");
//         }
//     }
// }
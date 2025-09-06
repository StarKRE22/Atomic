using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void IsVisible_False_ByDefault()
        {
            Assert.IsFalse(_collection.IsVisible, "По умолчанию коллекция не должна быть видимой");
        }

        [Test]
        public void IsVisible_True_AfterShow()
        {
            var entity = new Entity("Player");
            var source = new EntityCollection(entity);

            _collection.Show(source);

            Assert.IsTrue(_collection.IsVisible, "После вызова Show коллекция должна быть видимой");
        }

        [Test]
        public void IsVisible_False_AfterHide()
        {
            var entity = new Entity("Player");
            var source = new EntityCollection(entity);

            _collection.Show(source);
            _collection.Hide();

            Assert.IsFalse(_collection.IsVisible, "После вызова Hide коллекция должна стать невидимой");
        }
    }
}
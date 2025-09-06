using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        #region OnTagAdded

        [Test]
        public void OnTagAdded_IsInvoked_WhenTagIsAdded()
        {
            var entity = new Entity();
            int? receivedTag = null;
            IEntity receivedEntity = null;

            entity.OnTagAdded += (e, tag) =>
            {
                receivedEntity = e;
                receivedTag = tag;
            };

            entity.AddTag(42);

            Assert.AreEqual(42, receivedTag);
            Assert.AreSame(entity, receivedEntity);
        }

        [Test]
        public void OnTagAdded_IsNotInvoked_WhenTagAlreadyExists()
        {
            var entity = new Entity();
            entity.AddTag(1);

            bool wasCalled = false;
            entity.OnTagAdded += (_, _) => wasCalled = true;

            entity.AddTag(1); // Повторно

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnTagAdded_CanHandleMultipleTags()
        {
            var entity = new Entity();
            var addedTags = new List<int>();

            entity.OnTagAdded += (_, tag) => addedTags.Add(tag);

            entity.AddTag(10);
            entity.AddTag(20);
            entity.AddTag(30);

            CollectionAssert.AreEquivalent(new[] {10, 20, 30}, addedTags);
        }

        #endregion


        #region OnTagDeleted

        [Test]
        public void OnTagDeleted_IsInvoked_WhenTagIsRemoved()
        {
            var entity = new Entity();
            entity.AddTag(42);

            int? removedTag = null;
            IEntity sourceEntity = null;

            entity.OnTagDeleted += (e, tag) =>
            {
                sourceEntity = e;
                removedTag = tag;
            };

            entity.DelTag(42);

            Assert.AreEqual(42, removedTag);
            Assert.AreSame(entity, sourceEntity);
        }

        [Test]
        public void OnTagDeleted_IsNotInvoked_WhenTagDoesNotExist()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnTagDeleted += (_, _) => wasCalled = true;

            entity.DelTag(99); // Тег не добавлялся

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnTagDeleted_HandlesMultipleTagRemovals()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            var deletedTags = new List<int>();
            entity.OnTagDeleted += (_, tag) => deletedTags.Add(tag);

            entity.DelTag(1);
            entity.DelTag(2);
            entity.DelTag(3);

            CollectionAssert.AreEquivalent(new[] {1, 2, 3}, deletedTags);
        }

        #endregion

        #region TagCount

        [Test]
        public void TagCount_IsZero_WhenNoTagsAdded()
        {
            var entity = new Entity();

            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void TagCount_Increases_WhenTagIsAdded()
        {
            var entity = new Entity();
            entity.AddTag(100);

            Assert.AreEqual(1, entity.TagCount);
        }

        [Test]
        public void TagCount_DoesNotIncrease_WhenSameTagAddedTwice()
        {
            var entity = new Entity();
            entity.AddTag(10);
            entity.AddTag(10); // Повтор

            Assert.AreEqual(1, entity.TagCount);
        }

        [Test]
        public void TagCount_Decreases_WhenTagIsDeleted()
        {
            var entity = new Entity();
            entity.AddTag(5);
            entity.AddTag(6);

            entity.DelTag(5);

            Assert.AreEqual(1, entity.TagCount);
        }

        [Test]
        public void TagCount_DoesNotGoBelowZero_WhenDeletingNonexistentTag()
        {
            var entity = new Entity();
            entity.DelTag(999); // Ничего не было

            Assert.AreEqual(0, entity.TagCount);
        }

        #endregion

        #region HasTag

        [Test]
        public void HasTag()
        {
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            Entity e = new Entity("123");
            e.AddTags(new[] {tag1, tag2});

            Assert.IsTrue(e.HasTag(tag1));
            Assert.IsTrue(e.HasTag(tag2));
            Assert.IsFalse(e.HasTag(tag3));
        }

        [Test]
        public void HasTag_ReturnsFalse_WhenNoTags()
        {
            var entity = new Entity();

            Assert.IsFalse(entity.HasTag(1));
        }

        [Test]
        public void HasTag_ReturnsTrue_WhenTagExists()
        {
            var entity = new Entity();
            entity.AddTag(42);

            Assert.IsTrue(entity.HasTag(42));
        }

        [Test]
        public void HasTag_ReturnsFalse_WhenTagWasDeleted()
        {
            var entity = new Entity();
            entity.AddTag(7);
            entity.DelTag(7);

            Assert.IsFalse(entity.HasTag(7));
        }

        [Test]
        public void HasTag_IsAccurate_ForMultipleTags()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            Assert.IsTrue(entity.HasTag(2));
            Assert.IsFalse(entity.HasTag(99));
        }

        #endregion

        #region AddTag

        [Test]
        public void AddTag_ReturnsTrue_WhenTagIsNew()
        {
            var entity = new Entity();

            bool result = entity.AddTag(10);

            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag(10));
        }

        [Test]
        public void AddTag_ReturnsFalse_WhenTagAlreadyExists()
        {
            var entity = new Entity();
            entity.AddTag(42);

            bool result = entity.AddTag(42);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddTag_InvokesOnTagAdded_WhenNewTag()
        {
            var entity = new Entity();
            int? receivedTag = null;

            entity.OnTagAdded += (_, tag) => receivedTag = tag;

            entity.AddTag(7);

            Assert.AreEqual(7, receivedTag);
        }

        [Test]
        public void AddTag_DoesNotInvokeOnTagAdded_WhenDuplicate()
        {
            var entity = new Entity();
            entity.AddTag(5);

            bool wasCalled = false;
            entity.OnTagAdded += (_, _) => wasCalled = true;

            entity.AddTag(5);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void AddTag_InvokesOnStateChanged_WhenTagIsNew()
        {
            var entity = new Entity();
            bool stateChanged = false;

            entity.OnStateChanged += _ => stateChanged = true;

            entity.AddTag(123);

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void AddTag_DoesNotInvokeOnStateChanged_WhenTagExists()
        {
            var entity = new Entity();
            entity.AddTag(1);

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.AddTag(1); // дубликат

            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void AddTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int addedTag = -1;

            Entity e = new Entity("123");
            e.AddTags(new[] {tag1, tag2});
            e.OnTagAdded += (_, t) => addedTag = t;

            //Act:
            Assert.IsFalse(e.AddTag(tag1));
            Assert.AreEqual(-1, addedTag);

            Assert.IsFalse(e.AddTag(tag2));
            Assert.AreEqual(-1, addedTag);

            Assert.IsTrue(e.AddTag(tag3));
            Assert.AreEqual(tag3, addedTag);
        }

        #endregion

        #region DelTag

        [Test]
        public void DelTag_ReturnsTrue_WhenTagExisted()
        {
            var entity = new Entity();
            entity.AddTag(10);

            bool result = entity.DelTag(10);

            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasTag(10));
        }

        [Test]
        public void DelTag_ReturnsFalse_WhenTagDoesNotExist()
        {
            var entity = new Entity();

            bool result = entity.DelTag(999);

            Assert.IsFalse(result);
        }

        [Test]
        public void DelTag_InvokesOnTagDeleted_WhenSuccessful()
        {
            var entity = new Entity();
            entity.AddTag(5);

            int? deletedTag = null;
            entity.OnTagDeleted += (_, tag) => deletedTag = tag;

            entity.DelTag(5);

            Assert.AreEqual(5, deletedTag);
        }

        [Test]
        public void DelTag_DoesNotInvokeOnTagDeleted_WhenNotPresent()
        {
            var entity = new Entity();

            bool wasCalled = false;
            entity.OnTagDeleted += (_, _) => wasCalled = true;

            entity.DelTag(123);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void DelTag_InvokesOnStateChanged_WhenTagRemoved()
        {
            var entity = new Entity();
            entity.AddTag(7);

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.DelTag(7);

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void DelTag_DoesNotInvokeOnStateChanged_WhenTagNotFound()
        {
            var entity = new Entity();

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.DelTag(777); // не существует

            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void DelTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int removedTag = -1;

            Entity e = new Entity("123");
            e.AddTags(new[] {tag1, tag2});
            e.OnTagDeleted += (_, t) => removedTag = t;

            //Act & Assert:
            Assert.IsTrue(e.DelTag(tag1));
            Assert.AreEqual(tag1, removedTag);

            Assert.IsTrue(e.DelTag(tag2));
            Assert.AreEqual(tag2, removedTag);

            Assert.IsFalse(e.DelTag(tag3));
            Assert.AreNotEqual(tag3, removedTag);
        }

        #endregion

        #region GetTags

        [Test]
        public void GetTags_ReturnsAllTags_WhenTagsExist()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            var tags = entity.GetTags();

            CollectionAssert.AreEquivalent(new[] {1, 2, 3}, tags);
        }

        [Test]
        public void GetTags_ReturnsEmptyArray_WhenNoTags()
        {
            var entity = new Entity();

            var tags = entity.GetTags();

            Assert.IsNotNull(tags);
            Assert.IsEmpty(tags);
        }

        [Test]
        public void GetTags_ReturnedArrayIsIndependent()
        {
            var entity = new Entity();
            entity.AddTag(100);

            var tags = entity.GetTags();

            // Модифицируем состояние после получения
            entity.DelTag(100);

            // Результат не должен измениться
            CollectionAssert.AreEqual(new[] {100}, tags);
        }

        #endregion

        #region CopyTags

        [Test]
        public void CopyTags_CopiesAllTagsInOrder()
        {
            var entity = new Entity();
            entity.AddTag(10);
            entity.AddTag(20);
            entity.AddTag(30);

            var results = new int[3];
            int count = entity.CopyTags(results);

            Assert.AreEqual(3, count);
            CollectionAssert.AreEquivalent(new[] {10, 20, 30}, results);
        }

        [Test]
        public void CopyTags_ReturnsZero_WhenNoTags()
        {
            var entity = new Entity();
            var results = Array.Empty<int>();

            int count = entity.CopyTags(results);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void CopyTags_ThrowsArgumentNullException_WhenArrayIsNull()
        {
            var entity = new Entity();
            entity.AddTag(1);

            Assert.Throws<ArgumentNullException>(() => { entity.CopyTags(null); });
        }

        [Test]
        public void CopyTags_CopiesOnlyExistingTags_IgnoresDeleted()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);
            entity.DelTag(2); // удалим один

            var results = new int[3];
            int count = entity.CopyTags(results);

            Assert.AreEqual(2, count);
            CollectionAssert.AreEquivalent(new[] {1, 3}, results[..count]);
        }

        [Test]
        public void CopyTags_OverwritesGivenArrayFromStart()
        {
            var entity = new Entity();
            entity.AddTag(7);
            entity.AddTag(8);

            var results = new[] {-1, -1, -1};
            int count = entity.CopyTags(results);

            Assert.AreEqual(2, count);
            Assert.AreEqual(7, results[0]);
            Assert.AreEqual(8, results[1]);
            Assert.AreEqual(-1, results[2]); // не должен быть перезаписан
        }

        #endregion

        #region ClearTags

        [Test]
        public void ClearTags_RemovesAllTags()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            entity.ClearTags();

            Assert.AreEqual(0, entity.TagCount);
            Assert.IsFalse(entity.HasTag(1));
            Assert.IsFalse(entity.HasTag(2));
            Assert.IsFalse(entity.HasTag(3));
        }

        [Test]
        public void ClearTags_InvokesOnTagDeleted_ForEachTag()
        {
            var entity = new Entity();
            entity.AddTag(10);
            entity.AddTag(20);
            entity.AddTag(30);

            var deletedTags = new List<int>();
            entity.OnTagDeleted += (_, tag) => deletedTags.Add(tag);

            entity.ClearTags();

            CollectionAssert.AreEquivalent(new[] {10, 20, 30}, deletedTags);
        }

        [Test]
        public void ClearTags_InvokesOnStateChanged_Once()
        {
            var entity = new Entity();
            entity.AddTag(100);

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.ClearTags();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void ClearTags_DoesNothing_WhenNoTags()
        {
            var entity = new Entity();

            bool tagDeletedCalled = false;
            bool stateChangedCalled = false;

            entity.OnTagDeleted += (_, _) => tagDeletedCalled = true;
            entity.OnStateChanged += _ => stateChangedCalled = true;

            entity.ClearTags();

            Assert.IsFalse(tagDeletedCalled);
            Assert.IsFalse(stateChangedCalled);
            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void ClearTags()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;

            Entity e = new Entity("123");
            e.AddTags(new[] {tag1, tag2});

            //Act:
            e.ClearTags();

            //Assert:
            Assert.IsFalse(e.HasTag(tag1));
            Assert.IsFalse(e.HasTag(tag2));
        }

        #endregion

        #region GetTagEnumerator

        [Test]
        public void GetTagEnumerator_IteratesAllTags()
        {
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            var enumerator = entity.GetTagEnumerator();
            var collected = new List<int>();

            while (enumerator.MoveNext())
                collected.Add(enumerator.Current);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 3 }, collected);
        }

        [Test]
        public void GetTagEnumerator_ReturnsNoElements_WhenNoTags()
        {
            var entity = new Entity();

            var enumerator = entity.GetTagEnumerator();
            var collected = new List<int>();

            while (enumerator.MoveNext())
                collected.Add(enumerator.Current);

            Assert.IsEmpty(collected);
        }

        [Test]
        public void GetTagEnumerator_ResetsProperly()
        {
            var entity = new Entity();
            entity.AddTag(10);
            entity.AddTag(20);

            var enumerator = entity.GetTagEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            var first = enumerator.Current;

            enumerator.Reset();

            Assert.IsTrue(enumerator.MoveNext());
            var restarted = enumerator.Current;

            Assert.AreEqual(first, restarted);
        }

        #endregion
    }
}
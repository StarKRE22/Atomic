using NUnit.Framework;
using System;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityViewBaseTests
    {
        private GameObject _go;
        private TestEntityView _view;
        private Entity _entity;

        [SetUp]
        public void SetUp()
        {
            _go = new GameObject("View");
            _view = _go.AddComponent<TestEntityView>();
            _entity = new Entity();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(_go);
        }

        [Test]
        public void Show_SetsEntityAndIsVisible_AndCallsOnShow()
        {
            _view.Show(_entity);

            Assert.AreEqual(_entity, _view.Entity);
            Assert.IsTrue(_view.IsVisible);
            Assert.IsTrue(_view.OnShowCalled);
            Assert.IsTrue(_go.activeSelf);
        }

        [Test]
        public void Hide_ClearsEntityAndIsVisible_AndCallsOnHide()
        {
            _view.Show(_entity);
            _view.Hide();

            Assert.IsFalse(_view.IsVisible);
            Assert.IsNull(_view.Entity);
            Assert.IsTrue(_view.OnHideCalled);
            Assert.IsFalse(_go.activeSelf);
        }

        [Test]
        public void Show_NullEntity_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _view.Show(null));
        }

        [Test]
        public void Hide_WhenNotVisible_DoesNothing()
        {
            _view.Hide(); // Not visible yet

            Assert.IsFalse(_view.OnHideCalled); // Should not call OnHide
            Assert.IsFalse(_view.IsVisible);
        }

        [Test]
        public void ShowThenHideTwice_SecondHideDoesNotThrowOrChangeState()
        {
            _view.Show(_entity);
            _view.Hide();
            _view.Hide();

            Assert.IsFalse(_view.IsVisible);
            Assert.IsTrue(_view.OnHideCalled);
        }

        [Test]
        public void Show_EntityViewBaseNonGeneric_WorksAsExpected()
        {
            var nonGeneric = _go.AddComponent<TestEntityView>();
            nonGeneric.Show(_entity);
            Assert.AreEqual(_entity, nonGeneric.Entity);
            Assert.IsTrue(nonGeneric.IsVisible);
            nonGeneric.Hide();
            Assert.IsFalse(nonGeneric.IsVisible);
            Assert.IsNull(nonGeneric.Entity);
        }

        private class TestEntityView : EntityViewBase
        {
            public bool OnShowCalled { get; private set; }
            public bool OnHideCalled { get; private set; }

            protected override void OnShow(IEntity entity)
            {
                base.OnShow(entity);
                OnShowCalled = true;
            }

            protected override void OnHide(IEntity entity)
            {
                base.OnHide(entity);
                OnHideCalled = true;
            }
        }
    }
}
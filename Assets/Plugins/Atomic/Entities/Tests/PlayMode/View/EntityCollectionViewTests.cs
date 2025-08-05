using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Atomic.Entities
{
    public class EntityCollectionViewTests
    {
        private GameObject _go;
        private TestEntityCollectionView _view;
        private MockEntityViewPool _pool;
        private MockReadOnlyEntityCollection _collection;

        private Entity _e1, _e2;

        [SetUp]
        public void SetUp()
        {
            _go = new GameObject("EntityCollectionView");
            _view = _go.AddComponent<TestEntityCollectionView>();
            _pool = _go.AddComponent<MockEntityViewPool>();
            _collection = new MockReadOnlyEntityCollection();

            _view.SetDependencies(_pool, new GameObject("Viewport").transform);
            _e1 = new Entity("A");
            _e2 = new Entity("B");
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Show_SpawnsViews_ForAllEntities()
        {
            _collection.Add(_e1);
            _collection.Add(_e2);

            _view.Show(_collection);

            Assert.AreEqual(2, _view.ViewCount);
            Assert.AreEqual(_e1, _view.GetView(_e1).Entity);
            Assert.AreEqual(_e2, _view.GetView(_e2).Entity);
        }

        [Test]
        public void Show_RaisesOnAddedEvents()
        {
            List<IEntity> added = new();
            _view.OnAdded += (e, v) => added.Add(e);

            _collection.Add(_e1);
            _collection.Add(_e2);
            _view.Show(_collection);

            CollectionAssert.AreEquivalent(new[] {_e1, _e2}, added);
        }

        [Test]
        public void Hide_RemovesViews_AndRaisesOnRemoved()
        {
            _collection.Add(_e1);
            _collection.Add(_e2);
            _view.Show(_collection);

            List<IEntity> removed = new();
            _view.OnRemoved += (e, v) => removed.Add(e);

            _view.Hide();

            Assert.AreEqual(0, _view.ViewCount);
            CollectionAssert.AreEquivalent(new[] {_e1, _e2}, removed);
        }

        [Test]
        public void Show_ThrowsOnNull()
        {
            Assert.Throws<System.ArgumentNullException>(() => _view.Show(null));
        }

        [Test]
        public void Show_CalledTwice_IsSafe()
        {
            _collection.Add(_e1);
            _view.Show(_collection);
            _view.Show(_collection); // вторая попытка, должна перехватываться Hide()

            Assert.IsTrue(_view.HasView(_e1));
            Assert.AreEqual(1, _view.ViewCount);
        }

        public class MockEntityViewPool : EntityViewPoolAbstract<IEntity>
        {
            private Dictionary<string, EntityView> _rented = new();

            public override EntityViewBase<IEntity> Rent(string name)
            {
                var go = new GameObject($"View-{name}");
                var view = go.AddComponent<EntityView>();
                view.name = name;
                _rented[name] = view;
                return view;
            }

            public override void Return(string name, EntityViewBase<IEntity> view)
            {
                DestroyImmediate(view.gameObject);
                _rented.Remove(name);
            }

            public override void Clear()
            {
                throw new NotImplementedException();
            }
        }

        public class MockReadOnlyEntityCollection : IReadOnlyEntityCollection<IEntity>
        {
            private readonly List<IEntity> _entities = new();
            public event Action OnStateChanged;
            public event Action<IEntity> OnAdded;
            public event Action<IEntity> OnRemoved;
            public bool Contains(IEntity entity)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(ICollection<IEntity> results)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(IEntity[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public void Add(IEntity entity)
            {
                _entities.Add(entity);
                OnAdded?.Invoke(entity);
            }

            public void Remove(IEntity entity)
            {
                _entities.Remove(entity);
                OnRemoved?.Invoke(entity);
            }

            public IEnumerator<IEntity> GetEnumerator() => _entities.GetEnumerator();
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
            public int Count { get; }
        }

        public class TestEntityCollectionView : EntityCollectionView<IEntity>
        {
            public int ViewCount => GetPrivate<Dictionary<IEntity, EntityViewBase<IEntity>>>("_views").Count;

            public bool HasView(IEntity entity) => GetPrivate<Dictionary<IEntity, EntityViewBase<IEntity>>>("_views")
                .ContainsKey(entity);

            public void SetDependencies(EntityViewPoolAbstract<IEntity> pool, Transform viewport)
            {
                typeof(EntityCollectionView<IEntity>)
                    .GetField("_viewPool",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .SetValue(this, pool);

                typeof(EntityCollectionView<IEntity>)
                    .GetField("_viewport",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .SetValue(this, viewport);
            }

            public void SetSpawnCondition(Func<IEntity, bool> condition)
            {
                _customCondition = condition;
            }

            private Func<IEntity, bool> _customCondition;

            private T GetPrivate<T>(string field)
            {
                return (T) typeof(EntityCollectionView<IEntity>)
                    .GetField(field, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .GetValue(this);
            }
        }
    }
}
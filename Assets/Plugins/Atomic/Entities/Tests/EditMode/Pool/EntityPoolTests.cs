using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityPoolTests
    {
        private readonly IEntityFactory _factory = new EntityFactoryMock();
        private TestableEntityPool _pool;

        [SetUp]
        public void SetUp()
        {
            _pool = new TestableEntityPool(_factory);
        }

        [Test]
        public void Init_PopulatesPool()
        {
            _pool.Init(3);

            Assert.AreEqual(3, _pool.PoolCount);
            Assert.AreEqual(3, _pool.Created.Count);
        }

        [Test]
        public void Rent_FromInitializedPool_ReturnsEntityAndCallsOnRent()
        {
            _pool.Init(1);
            IEntity entity = _pool.Rent();

            Assert.NotNull(entity);
            Assert.AreEqual(0, _pool.PoolCount);
            CollectionAssert.Contains(_pool.Rented, entity);
            Assert.AreEqual(entity, _pool.OnRentCalledWith[0]);
        }

        [Test]
        public void Rent_WhenPoolIsEmpty_CreatesNewEntityAndCallsOnCreate()
        {
            var entity = _pool.Rent();

            Assert.NotNull(entity);
            Assert.AreEqual(1, _pool.Created.Count);
            CollectionAssert.Contains(_pool.Rented, entity);
        }

        [Test]
        public void Return_ValidEntity_ReturnsToPoolAndCallsOnReturn()
        {
            var entity = _pool.Rent();
            _pool.Return(entity);

            Assert.AreEqual(1, _pool.PoolCount);
            Assert.AreEqual(0, _pool.Rented.Count);
            Assert.AreEqual(entity, _pool.OnReturnCalledWith[0]);
        }

        [Test]
        public void Return_UntrackedEntity_DoesNothing()
        {
            var entity = new Entity();
            _pool.Return(entity);

            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(0, _pool.OnReturnCalledWith.Count);
        }

        [Test]
        public void Dispose_ClearsPoolAndCallsOnDispose()
        {
            _pool.Init(2);
            _pool.Dispose();

            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(2, _pool.OnDisposeCalledWith.Count);
        }

        [Test]
        public void Rent_Return_RentAgain_ReturnsSameEntity()
        {
            var entity = _pool.Rent();
            _pool.Return(entity);
            var rentedAgain = _pool.Rent();

            Assert.AreSame(entity, rentedAgain);
        }

        [Test]
        public void Constructor_NullFactory_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new TestableEntityPool(null));
        }

        [Test]
        public void Init_ZeroCount_DoesNotCreate()
        {
            _pool.Init(0);
            Assert.AreEqual(0, _pool.Created.Count);
            Assert.AreEqual(0, _pool.PoolCount);
        }

        [Test]
        public void Return_SameEntityTwice_SecondTimeIgnored()
        {
            var entity = _pool.Rent();
            _pool.Return(entity);
            _pool.Return(entity);

            Assert.AreEqual(1, _pool.PoolCount);
            Assert.AreEqual(1, _pool.OnReturnCalledWith.Count);
        }

        [Test]
        public void Rent_MultipleEntities_AllTracked()
        {
            _pool.Init(3);
            var e1 = _pool.Rent();
            var e2 = _pool.Rent();
            var e3 = _pool.Rent();

            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(3, _pool.Rented.Count);
            CollectionAssert.AreEquivalent(new[] {e1, e2, e3}, _pool.Rented);
        }

        [Test]
        public void Return_MultipleEntities_AllBackInPool()
        {
            var e1 = _pool.Rent();
            var e2 = _pool.Rent();
            _pool.Return(e1);
            _pool.Return(e2);

            Assert.AreEqual(2, _pool.PoolCount);
            Assert.AreEqual(0, _pool.Rented.Count);
        }

        [Test]
        public void Dispose_ClearsRentedEntitiesToo()
        {
            var e = _pool.Rent();
            _pool.Dispose();

            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(0, _pool.Rented.Count);
            Assert.AreEqual(1, _pool.OnDisposeCalledWith.Count);
            Assert.AreEqual(e, _pool.OnDisposeCalledWith[0]);
        }

        [Test]
        public void Hooks_AreCalledInCorrectSequence()
        {
            _pool.Init(1);
            var e = _pool.Rent();

            CollectionAssert.AreEqual(new[] {e}, _pool.Created);
            CollectionAssert.AreEqual(new[] {e}, _pool.OnRentCalledWith);

            _pool.Return(e);
            CollectionAssert.AreEqual(new[] {e}, _pool.OnReturnCalledWith);

            _pool.Dispose();
            CollectionAssert.AreEqual(new[] {e}, _pool.OnDisposeCalledWith);
        }

        [Test]
        public void Rent_AfterDispose_CreatesNewEntity()
        {
            _pool.Init(1);
            _pool.Dispose();

            var newEntity = _pool.Rent();
            Assert.AreEqual(1, _pool.Created.Count);
            Assert.AreEqual(newEntity, _pool.Created[0]);
        }

        [Test]
        public void Init_WithZero_DoesNothing()
        {
            _pool.Init(0);

            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(0, _pool.Created.Count);
        }

        [Test]
        public void Init_WithNegativeCount_DoesNotCrash()
        {
            // На случай если кто-то решит "Init(-1)" – проверим.
            _pool.Init(-5);
            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(0, _pool.Created.Count);
        }

        [Test]
        public void Rent_MultipleEntities_AllDifferent()
        {
            _pool.Init(3);

            var e1 = _pool.Rent();
            var e2 = _pool.Rent();
            var e3 = _pool.Rent();

            Assert.AreNotSame(e1, e2);
            Assert.AreNotSame(e1, e3);
            Assert.AreNotSame(e2, e3);
        }

        [Test]
        public void Return_SameEntityTwice_SecondReturnIgnored()
        {
            var entity = _pool.Rent();
            _pool.Return(entity);
            _pool.Return(entity); // второй раз вернуть не получится

            Assert.AreEqual(1, _pool.PoolCount);
            Assert.AreEqual(1, _pool.OnReturnCalledWith.Count);
        }

        [Test]
        public void Rent_AfterDispose_CreatesNewEntities()
        {
            _pool.Init(2);
            _pool.Dispose();

            var newEntity = _pool.Rent();
            Assert.AreEqual(1, _pool.Created.Count); // 1 новый после Dispose
            CollectionAssert.Contains(_pool.Rented, newEntity);
        }

        [Test]
        public void Return_ThenRent_ReturnsSameEntity()
        {
            _pool.Init(1);
            var entity = _pool.Rent();
            _pool.Return(entity);
            var rentedAgain = _pool.Rent();

            Assert.AreSame(entity, rentedAgain);
            Assert.AreEqual(2, _pool.OnRentCalledWith.Count);
        }

        [Test]
        public void Rent_MultipleCycles_SameEntityReused()
        {
            _pool.Init(1);
            var entity = _pool.Rent();
            _pool.Return(entity);

            for (int i = 0; i < 5; i++)
            {
                var next = _pool.Rent();
                Assert.AreSame(entity, next);
                _pool.Return(next);
            }

            Assert.AreEqual(6, _pool.OnRentCalledWith.Count); // 1 + 5
            Assert.AreEqual(6, _pool.OnReturnCalledWith.Count); // 1 + 5
        }

        [Test]
        public void Return_NullEntity_DoesNothing()
        {
            _pool.Return(null);
            Assert.AreEqual(0, _pool.PoolCount);
            Assert.AreEqual(0, _pool.OnReturnCalledWith.Count);
        }

        [Test]
        public void Rent_CallsFactoryEachTimeWhenPoolEmpty()
        {
            var e1 = _pool.Rent();
            var e2 = _pool.Rent(); // Пул пуст -> создаем новый
            Assert.AreEqual(2, _pool.Created.Count);
            Assert.AreNotSame(e1, e2);
        }

        private sealed class TestableEntityPool : EntityPool<IEntity>
        {
            public List<IEntity> Created { get; } = new();
            public List<IEntity> OnRentCalledWith { get; } = new();
            public List<IEntity> OnReturnCalledWith { get; } = new();
            public List<IEntity> OnDisposeCalledWith { get; } = new();
            public HashSet<IEntity> Rented => _rentEntities;
            public int PoolCount => _pooledEntities.Count;

            public TestableEntityPool(IEntityFactory factory) : base(factory)
            {
            }

            protected override void OnCreate(IEntity entity) => Created.Add(entity);
            protected override void OnRent(IEntity entity) => OnRentCalledWith.Add(entity);
            protected override void OnReturn(IEntity entity) => OnReturnCalledWith.Add(entity);
            protected override void OnDispose(IEntity entity)
            {
                Debug.Log("DISPOSE ENTITY");
                Created.Remove(entity);
                OnDisposeCalledWith.Add(entity);
            }
        }
    }
}
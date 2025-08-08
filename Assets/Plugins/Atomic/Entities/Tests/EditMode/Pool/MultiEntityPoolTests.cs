using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public class MultiEntityPoolTests
    {
        private readonly MultiEntityFactoryMock factory = new()
        {
            CreateMethod = _ => new Entity()
        };
        
        private TestMultiEntityPool<string, IEntity> _pool;

        [SetUp]
        public void SetUp()
        {
            _pool = new TestMultiEntityPool<string, IEntity>(factory);
        }

        [Test]
        public void Init_CreatesEntitiesInPool()
        {
            _pool.Init("enemy", 3);
            Assert.AreEqual(3, _pool.GetPoolCount("enemy"));
            Assert.AreEqual(3, _pool.OnCreateList.Count);
        }

        [Test]
        public void Rent_FromPool_ReturnsEntity()
        {
            _pool.Init("bullet", 1);
            var entity = _pool.Rent("bullet");

            Assert.NotNull(entity);
            Assert.IsTrue(_pool.IsRented(entity));
            Assert.AreEqual(entity, _pool.OnRentList[0]);
        }

        [Test]
        public void Rent_EmptyPool_CreatesNewEntity()
        {
            var entity = _pool.Rent("fx");
            Assert.NotNull(entity);
            Assert.AreEqual(1, _pool.OnCreateList.Count);
            Assert.IsTrue(_pool.IsRented(entity));
        }

        [Test]
        public void Return_ValidEntity_AddsBackToPool()
        {
            var entity = _pool.Rent("unit");
            _pool.Return(entity);

            Assert.AreEqual(1, _pool.GetPoolCount("unit"));
            Assert.AreEqual(1, _pool.OnReturnList.Count);
            Assert.IsFalse(_pool.IsRented(entity));
        }

        [Test]
        public void Return_UntrackedEntity_DoesNothing()
        {
            var entity = new Entity();
            _pool.Return(entity);

            Assert.AreEqual(0, _pool.OnReturnList.Count);
        }

        [Test]
        public void Return_SameEntityTwice_SecondIgnored()
        {
            var entity = _pool.Rent("explosion");
            _pool.Return(entity);
            _pool.Return(entity);

            Assert.AreEqual(1, _pool.GetPoolCount("explosion"));
            Assert.AreEqual(1, _pool.OnReturnList.Count);
        }

        [Test]
        public void Dispose_ClearsAllPoolsAndRentedEntities()
        {
            var e1 = _pool.Rent("a");
            var e2 = _pool.Rent("b");


            _pool.Return(e1);
            _pool.Dispose();

            Assert.AreEqual(2, _pool.OnDisposeList.Count);
            Assert.AreEqual(0, _pool.TotalPoolCount);
        }

        public class TestMultiEntityPool<TKey, E> : MultiEntityPool<TKey, E> where E : IEntity
        {
            public List<E> OnCreateList = new();
            public List<E> OnRentList = new();
            public List<E> OnReturnList = new();
            public List<E> OnDisposeList = new();

            public TestMultiEntityPool(IMultiEntityFactory<TKey, E> factory) : base(factory)
            {
            }

            protected override void OnCreate(E entity) => OnCreateList.Add(entity);
            protected override void OnRent(E entity) => OnRentList.Add(entity);
            protected override void OnReturn(E entity) => OnReturnList.Add(entity);
            protected override void OnDispose(E entity) => OnDisposeList.Add(entity);

            public int GetPoolCount(TKey key)
            {
                var field = typeof(MultiEntityPool<TKey, E>).GetField("_pooledEntities",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var dict = (Dictionary<TKey, Stack<E>>) field.GetValue(this);
                return dict.TryGetValue(key, out var stack) ? stack.Count : 0;
            }

            public int TotalPoolCount
            {
                get
                {
                    var field = typeof(MultiEntityPool<TKey, E>).GetField("_pooledEntities",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var dict = (Dictionary<TKey, Stack<E>>) field.GetValue(this);
                    int count = 0;
                    foreach (var stack in dict.Values)
                        count += stack.Count;
                    return count;
                }
            }

            public bool IsRented(E entity)
            {
                var field = typeof(MultiEntityPool<TKey, E>).GetField("_rentEntities",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var dict = (Dictionary<E, TKey>) field.GetValue(this);
                return dict.ContainsKey(entity);
            }
        }
    }
}
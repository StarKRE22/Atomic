using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnStateChanged_Tests
    {
        [Test]
        public void Enable_Raises_OnStateChanged()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnStateChanged += () => called = true;

            world.Enable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_Raises_OnStateChanged()
        {
            var world = new EntityWorld<Entity>();
            world.Enable(); // чтобы _enabled = true
            bool called = false;
            world.OnStateChanged += () => called = true;

            world.Disable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Add_Raises_OnStateChanged()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnStateChanged += () =>
            {
                Debug.Log("AAA");
                called = true;
            };

            world.Add(new Entity());

            Assert.IsTrue(called);
        }

        [Test]
        public void Remove_Raises_OnStateChanged()
        {
            var entity = new Entity();
            var world = new EntityWorld<Entity>(entity);

            bool called = false;
            world.OnStateChanged += () => called = true;

            world.Remove(entity);

            Assert.IsTrue(called, "OnStateChanged should be called on Remove.");
        }

        [Test]
        public void Clear_Raises_OnStateChanged()
        {
            var world = new EntityWorld<Entity>(new Entity(), new Entity());

            bool called = false;
            world.OnStateChanged += () => called = true;

            world.Clear();

            Assert.IsTrue(called, "OnStateChanged should be called on Clear.");
        }

        [Test]
        public void Dispose_Raises_OnStateChanged()
        {
            var world = new EntityWorld<Entity>(new Entity());
            world.Enable(); // Ensure state changes on Dispose

            bool called = false;
            world.OnStateChanged += () => called = true;

            world.Dispose();

            Assert.IsTrue(called, "OnStateChanged should be called on Dispose.");
        }
    }
}
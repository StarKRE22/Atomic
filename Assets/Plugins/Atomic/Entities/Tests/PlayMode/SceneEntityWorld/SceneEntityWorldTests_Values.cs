using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed partial class SceneEntityWorldTests
    {
        [Test]
        public void GetEntityWithValue()
        {
            var entity1 = new Entity("1", values: new Dictionary<int, object>
            {
                {0, new object()}
            });
            var entity2 = new Entity("2", values: new Dictionary<int, object>
            {
                {1, new object()}
            });
            var entity3 = new Entity("3", values: new Dictionary<int, object>
            {
                {1, new object()}
            });

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false,
                entity2, entity1, entity3);

            //Act
            bool sucess = entityWorld.GetWithValue(1, out IEntity entityWithValue);

            //Assert
            Assert.IsTrue(sucess);
            Assert.AreEqual(entity2, entityWithValue);
        }
    }
}
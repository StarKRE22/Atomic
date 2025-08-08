using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class SceneEntityBaker_BakeAll_Tests
    {
        [SetUp]
        public void SetUp()
        {
            SceneEntityBakerDummy.CreateCallCount = 0;

            // Очистим сцену от лишнего
            foreach (var obj in Object.FindObjectsOfType<GameObject>())
            {
                if (Application.isPlaying)
                    Object.Destroy(obj);
            }
        }

        [UnityTest]
        public IEnumerator BakeAll_Should_Bake_Active_Objects()
        {
            // Arrange
            for (int i = 0; i < 3; i++)
            {
                new GameObject($"Baker_{i}", typeof(SceneEntityBakerDummy));
            }

            yield return null; // allow frame to register GameObjects

            // Act
            var entities = SceneEntityBakerDummy.BakeAll();

            //Wait for destroy bakers:
            yield return null;

            // Assert
            Assert.AreEqual(3, entities.Length);
            Assert.AreEqual(3, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerDummy>().Length);
        }

        [UnityTest]
        public IEnumerator BakeAll_Should_Bake_Inactive_If_IncludeInactive_True()
        {
            // Arrange
            var go = new GameObject("InactiveBaker", typeof(SceneEntityBakerDummy));
            go.SetActive(false);

            yield return null; // allow frame to register GameObjects

            // Act
            var entities = SceneEntityBakerDummy.BakeAll(includeInactive: true);

            //Wait for destroy bakers:
            yield return null;

            // Assert
            Assert.AreEqual(1, entities.Length);
            Assert.AreEqual(1, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerDummy>(true).Length);
        }

        [UnityTest]
        public IEnumerator BakeAll_Should_Skip_Inactive_If_IncludeInactive_False()
        {
            // Arrange
            var go = new GameObject("InactiveBaker", typeof(SceneEntityBakerDummy));
            go.SetActive(false);

            yield return null;

            // Act
            var entities = SceneEntityBakerDummy.BakeAll(includeInactive: false);

            // Assert
            Assert.AreEqual(0, entities.Length);
            Assert.AreEqual(0, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(1, Object.FindObjectsOfType<SceneEntityBakerDummy>(true).Length);
        }
    }
}
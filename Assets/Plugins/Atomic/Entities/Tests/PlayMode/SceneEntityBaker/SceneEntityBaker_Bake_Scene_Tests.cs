using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class SceneEntityBaker_Bake_Scene_Tests
    {
        private Scene _testScene;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneEntityBakerDummy.CreateCallCount = 0;

            // Создаём пустую сцену
            _testScene = SceneManager.CreateScene("BakeTestScene");
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_testScene.IsValid() && _testScene.isLoaded)
                yield return SceneManager.UnloadSceneAsync(_testScene);
        }

        [UnityTest]
        public IEnumerator Bake_Should_Bake_All_Entities_In_Scene()
        {
            // Arrange
            for (int i = 0; i < 3; i++)
            {
                var go = new GameObject($"Baker_{i}", typeof(SceneEntityBakerDummy));
                SceneManager.MoveGameObjectToScene(go, _testScene);
            }

            yield return null;

            // Act
            var baked = SceneEntityBakerDummy.Bake(_testScene);

            //Wait for destroy bakers:
            yield return null;

            // Assert
            Assert.AreEqual(3, baked.Count);
            Assert.AreEqual(3, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerDummy>(true).Length);
        }

        [UnityTest]
        public IEnumerator Bake_Should_Respect_IncludeInactive_Flag()
        {
            // Arrange
            var go = new GameObject("InactiveBaker", typeof(SceneEntityBakerDummy));
            go.SetActive(false);
            SceneManager.MoveGameObjectToScene(go, _testScene);

            yield return null;
            yield return null;
            yield return null;

            // Act
            var baked = SceneEntityBakerDummy.Bake(_testScene, includeInactive: false);

            //Wait for destroy bakers:
            yield return null;

            // Assert
            Assert.AreEqual(0, baked.Count);
            Assert.AreEqual(0, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(1, Object.FindObjectsOfType<SceneEntityBakerDummy>(true).Length);
        }

        [UnityTest]
        public IEnumerator Bake_Should_Bake_Inactive_When_IncludeInactive_True()
        {
            // Arrange
            var go = new GameObject("InactiveBaker", typeof(SceneEntityBakerDummy));
            go.SetActive(false);
            SceneManager.MoveGameObjectToScene(go, _testScene);

            yield return null;

            // Act
            var baked = SceneEntityBakerDummy.Bake(_testScene, includeInactive: true);

            //Wait for destroy bakers:
            yield return null;

            // Assert
            Assert.AreEqual(1, baked.Count);
            Assert.AreEqual(1, SceneEntityBakerDummy.CreateCallCount);
            Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerDummy>(true).Length);
        }
    }
}
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_InstallEntities
    {
        private Scene _scene;

        [AddComponentMenu("")]
        private class TestEntity : MonoEntity
        {
            public int InstallCallCount;

            public TestEntity()
            {
                this.installOnAwake = false;
            }

            protected override void OnInstall()
            {
                InstallCallCount++;
            }
        }

        [UnitySetUp]
        public IEnumerator SetupScene()
        {
            _scene = SceneManager.CreateScene("TestScene");
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TeardownScene()
        {
            yield return SceneManager.UnloadSceneAsync(_scene);
        }

        [UnityTest]
        public IEnumerator InstallAll_InstallsAllEntitiesInScene()
        {
            var entity1 = new GameObject("Entity1").AddComponent<TestEntity>();
            SceneManager.MoveGameObjectToScene(entity1.gameObject, _scene);

            var entity2 = new GameObject("Entity2").AddComponent<TestEntity>();
            SceneManager.MoveGameObjectToScene(entity2.gameObject, _scene);

            Assert.IsFalse(entity1.Installed);
            Assert.IsFalse(entity1.Installed);

            MonoEntity.InstallAll<TestEntity>(_scene);

            Assert.IsTrue(entity1.Installed);
            Assert.IsTrue(entity2.Installed);

            Assert.AreEqual(1, entity1.InstallCallCount);
            Assert.AreEqual(1, entity1.InstallCallCount);

            yield return null;
        }

        [UnityTest]
        public IEnumerator InstallAll_SkipsAlreadyInstalledEntities()
        {
            var go = new GameObject("Root");
            SceneManager.MoveGameObjectToScene(go, _scene);

            var entity = go.AddComponent<TestEntity>();
            entity.Install(); // вручную

            MonoEntity.InstallAll<TestEntity>(_scene);

            Assert.AreEqual(1, entity.InstallCallCount); // только один вызов
            yield return null;
        }

        [UnityTest]
        public IEnumerator InstallAll_IgnoresOtherSceneObjects()
        {
            //Arrange
            var otherScene = SceneManager.CreateScene("OtherScene");
            var go = new GameObject("OtherRoot");
            var entity = go.AddComponent<TestEntity>();
            SceneManager.MoveGameObjectToScene(go, otherScene);

            //Check
            Assert.IsFalse(entity.Installed);
            
            //Act
            MonoEntity.InstallAll<TestEntity>(_scene); // вызываем не на той сцене

            //Assert
            Assert.IsFalse(entity.Installed);
            Assert.AreEqual(0, entity.InstallCallCount);
            yield return SceneManager.UnloadSceneAsync(otherScene);
        }

        [UnityTest]
        public IEnumerator InstallAll_WorksWithDerivedTypes()
        {
            var go = new GameObject("Root");
            SceneManager.MoveGameObjectToScene(go, _scene);

            var derived = go.AddComponent<TestEntity>();

            MonoEntity.InstallAll<MonoEntity>(_scene);

            Assert.IsTrue(derived.Installed);
            Assert.AreEqual(1, derived.InstallCallCount);
            yield return null;
        }

        [UnityTest]
        public IEnumerator InstallAll_NoEntities_DoesNotThrow()
        {
            var go = new GameObject("Root");
            SceneManager.MoveGameObjectToScene(go, _scene);

            LogAssert.NoUnexpectedReceived();
            MonoEntity.InstallAll<TestEntity>(_scene); // нет сущностей — всё ок

            yield return null;
        }
    }
}
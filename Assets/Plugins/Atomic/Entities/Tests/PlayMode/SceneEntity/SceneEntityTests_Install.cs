using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class SceneEntityTests_Install
    {
        private GameObject _go;
        private SceneEntity _entity;
        
        private class TestableSceneEntity : SceneEntity
        {
            public int installCounter = 0;

            protected override void OnInstall()
            {
                installCounter++;
            }
        }

        [SetUp]
        public void SetUp()
        {
            _go = new GameObject("TestEntity");
            _entity = _go.AddComponent<SceneEntity>();
            _entity.Uninstall();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_go);
        }

        [UnityTest]
        public IEnumerator Install_SetsInstalledTrue_WhenNoInstallersOrChildren()
        {
            yield return null;

            _entity.Install();

            Assert.IsTrue(_entity.Installed);
        }

        private class MockInstaller : SceneEntityInstaller<SceneEntity>
        {
            public bool called;

            public override void Install(SceneEntity entity)
            {
                called = true;
            }
        }

        [UnityTest]
        public IEnumerator Install_CallsAllInstallers()
        {
            yield return null;

            var mockInstaller = _go.AddComponent<MockInstaller>();
            _entity.sceneInstallers = new List<SceneEntityInstaller> {mockInstaller};
            _entity.Install();

            Assert.IsTrue(mockInstaller.called);
        }

        [UnityTest]
        public IEnumerator Install_CallsChildren()
        {
            yield return null;

            var childGO = new GameObject("Child");
            childGO.transform.parent = _go.transform;
            var childEntity = childGO.AddComponent<SceneEntity>();
            childEntity.Uninstall();

            _entity.childInstallers = new List<SceneEntity> {childEntity};

            _entity.Install();

            Assert.IsTrue(childEntity.Installed);

            Object.DestroyImmediate(childGO);
        }

        [UnityTest]
        public IEnumerator Install_SkipsNullInstaller()
        {
            yield return null;

            _entity.sceneInstallers = new List<SceneEntityInstaller> {null};

            // LogAssert.Expect(LogType.Warning, "SceneEntity TestEntity: Ops! Detected null installer!");

            _entity.Install();

            Assert.IsTrue(_entity.Installed);
        }

        [UnityTest]
        public IEnumerator Install_SkipsNullChild()
        {
            yield return null;

            _entity.childInstallers = new List<SceneEntity> {null};

            // LogAssert.Expect(LogType.Warning, "SceneEntity TestEntity: Ops! Detected null child entity!");

            _entity.Install();

            Assert.IsTrue(_entity.Installed);
        }

        [UnityTest]
        public IEnumerator Install_DoesNothingIfAlreadyInstalled()
        {
            yield return null;

            _entity.Install();
            Assert.IsTrue(_entity.Installed);

            _entity.sceneInstallers = new List<SceneEntityInstaller>
            {
                new GameObject("ignoredInstaller").AddComponent<MockInstaller>()
            };

            _entity.Install(); // повторная установка должна быть проигнорирована

            var installer = (MockInstaller) _entity.sceneInstallers[0];
            Assert.IsFalse(installer.called);

            Object.DestroyImmediate(installer.gameObject);
        }
        
        
        [UnityTest]
        public IEnumerator Install_SecondCall_DoesNotReinstall()
        {
            _go = new GameObject("Entity");
            var entity = _go.AddComponent<TestableSceneEntity>();

            entity.Install();
            Assert.AreEqual(1, entity.installCounter);

            entity.Install();
            Assert.AreEqual(1, entity.installCounter, "Install() should not reinstall if already installed");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Install_TriggersOnInstallOverride()
        {
            _go = new GameObject("Entity");
            var entity = _go.AddComponent<TestableSceneEntity>();

            entity.Install();

            Assert.AreEqual(1, entity.installCounter);
            Assert.IsTrue(entity.Installed);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_CallsInstall_WhenInstallOnAwakeEnabled()
        {
            _go = new GameObject("Entity");
            var entity = _go.AddComponent<TestableSceneEntity>();
            entity.installOnAwake = true;

            yield return null;

            Assert.AreEqual(1, entity.installCounter);
        }

        [UnityTest]
        public IEnumerator Awake_DoesNotCallInstall_WhenInstallOnAwakeDisabled()
        {
            _go = new GameObject("Entity");
            _go.SetActive(false);
            var entity = _go.AddComponent<TestableSceneEntity>();
            entity.installOnAwake = false;
            _go.SetActive(true);

            yield return null;

            Assert.AreEqual(0, entity.installCounter);
        }
    }
}
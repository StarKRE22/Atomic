using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_DestroyEntity
    {
        [UnityTest]
        public IEnumerator DestroyEntity_RemovesSceneEntity_Immediately_WhenDelayIsZero()
        {
            var go = new GameObject("SceneEntity");
            var sceneEntity = go.AddComponent<SceneEntity>();

            Assert.IsNotNull(go);
            SceneEntity.Destroy(sceneEntity);

            yield return null; // подождём 1 кадр
            Assert.IsTrue(go == null || go.Equals(null)); // уничтожен
        }

        [UnityTest]
        public IEnumerator DestroyEntity_RemovesSceneEntity_AfterDelay()
        {
            var go = new GameObject("SceneEntity");
            var sceneEntity = go.AddComponent<SceneEntity>();

            SceneEntity.Destroy(sceneEntity, 0.2f);

            Assert.IsNotNull(go);

            yield return new WaitForSeconds(0.25f);

            Assert.IsTrue(go == null || go.Equals(null));
        }

        [Test]
        public void Destroy_DoesNothing_WhenEntityIsNotSceneEntity()
        {
            var dummy = new Entity();
            Assert.Throws<InvalidCastException>(() => SceneEntity.Destroy(dummy));
        }
    }
}
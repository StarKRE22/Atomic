using System;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_CastEntity
    {
        #region Cast

        [Test]
        public void CastEntity_NullEntity_ReturnsNull()
        {
            // Act
            var result = SceneEntity.Cast(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CastEntity_SceneEntity_ReturnsSameInstance()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<SceneEntity>();

            // Act
            var result = SceneEntity.Cast(sceneEntity);

            // Assert
            Assert.AreSame(sceneEntity, result);
        }

        [Test]
        public void CastEntity_SceneEntityProxy_ReturnsSource()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<SceneEntity>();
            var proxy = gameObject.AddComponent<SceneEntityProxy>();
            proxy.Source = sceneEntity;

            // Act
            var result = SceneEntity.Cast(proxy);

            // Assert
            Assert.AreSame(sceneEntity, result);
        }

        [Test]
        public void CastEntity_EntityNotSceneEntityOrProxy_ThrowsInvalidCastException()
        {
            // Arrange
            var entity = new Entity();

            // Act & Assert
            Assert.Throws<InvalidCastException>(() => SceneEntity.Cast(entity));
        }
        
        [Test]
        public void CastEntity_EntityWrongType_ThrowsAndNotEqual()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<SceneEntity>();
            var entity = new Entity();

            // Act
            SceneEntity casted = null;
            try
            {
                casted = SceneEntity.Cast(entity);
            }
            catch
            {
                // Expected exception
            }

            // Assert
            Assert.AreNotEqual(sceneEntity, casted);
        }

        #endregion

        #region TryCast
        
        [Test]
        public void TryCast_ReturnsFalse_IfEntityIsNull()
        {
            var result = SceneEntity.TryCast(null, out SceneEntity casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_ReturnsFalse_IfEntityIsNotSceneEntity()
        {
            var dummy = new EntityDummy();
            var result = SceneEntity.TryCast(dummy, out SceneEntity casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_ReturnsTrue_ButCastedIsNull_WhenProxySourceIsNull()
        {
            var proxy = new GameObject("Proxy").AddComponent<SceneEntityProxy>();
            proxy.Source = null;

            var result = SceneEntity.TryCast(proxy, out SceneEntity casted);
            Assert.IsTrue(result);  // TryCast matches the proxy type
            Assert.IsNull(casted);  // But Source was null
        }

        [Test]
        public void TryCast_Generic_ReturnsFalse_IfSceneEntityIsWrongType()
        {
            var instance = new GameObject("Instance")
                .AddComponent<SceneEntityDummy_Other>();

            var result = SceneEntity.TryCast(instance, out SceneEntityDummy_Another casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_Generic_ReturnsFalse_IfProxyGenericTypeIsWrong()
        {
            var proxy = new GameObject("Proxy")
                .AddComponent<SceneEntityProxy<SceneEntityDummy_Other>>();

            var result = SceneEntity.TryCast(proxy, out SceneEntityDummy_Another casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }
        
        
        [Test]
        public void TryCast_ReturnsTrue_WhenDirectInstanceOfSceneEntity()
        {
            IEntity entity = new GameObject("MySceneEntity").AddComponent<SceneEntityDummy>();

            bool result = SceneEntity.TryCast(entity, out SceneEntity casted);

            Assert.IsTrue(result);
            Assert.AreEqual(entity, casted);
        }

        [Test]
        public void TryCast_Generic_ReturnsTrue_WhenCorrectGenericType()
        {
            IEntity entity = new GameObject("MySceneEntity")
                .AddComponent<SceneEntityDummy>();

            bool result = SceneEntity.TryCast(entity, out SceneEntityDummy casted);

            Assert.IsTrue(result);
            Assert.AreEqual(entity, casted);
        }

        [Test]
        public void TryCast_ReturnsTrue_WhenProxyMatchesGeneric()
        {
            var real = new GameObject("RealEntity").AddComponent<SceneEntityDummy>();
            var proxy = new GameObject("Proxy").AddComponent<SceneEntityProxyDummy>();
            proxy.Source = real;

            bool result = SceneEntity.TryCast(proxy, out SceneEntityDummy casted);

            Assert.IsTrue(result);
            Assert.AreEqual(real, casted);
        }

        #endregion
    }
}
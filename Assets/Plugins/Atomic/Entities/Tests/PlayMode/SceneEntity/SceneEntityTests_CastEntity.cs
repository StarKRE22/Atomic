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
            var result = MonoEntity.Cast(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CastEntity_SceneEntity_ReturnsSameInstance()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<MonoEntity>();

            // Act
            var result = MonoEntity.Cast(sceneEntity);

            // Assert
            Assert.AreSame(sceneEntity, result);
        }

        [Test]
        public void CastEntity_MonoEntityProxy_ReturnsSource()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<MonoEntity>();
            var proxy = gameObject.AddComponent<MonoEntityProxy>();
            proxy.Source = sceneEntity;

            // Act
            var result = MonoEntity.Cast(proxy);

            // Assert
            Assert.AreSame(sceneEntity, result);
        }

        [Test]
        public void CastEntity_EntityNotSceneEntityOrProxy_ThrowsInvalidCastException()
        {
            // Arrange
            var entity = new Entity();

            // Act & Assert
            Assert.Throws<InvalidCastException>(() => MonoEntity.Cast(entity));
        }
        
        [Test]
        public void CastEntity_EntityWrongType_ThrowsAndNotEqual()
        {
            // Arrange
            var gameObject = new GameObject();
            var sceneEntity = gameObject.AddComponent<MonoEntity>();
            var entity = new Entity();

            // Act
            MonoEntity casted = null;
            try
            {
                casted = MonoEntity.Cast(entity);
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
            var result = MonoEntity.TryCast(null, out MonoEntity casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_ReturnsFalse_IfEntityIsNotSceneEntity()
        {
            var dummy = new EntitySpy();
            var result = MonoEntity.TryCast(dummy, out MonoEntity casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_ReturnsTrue_ButCastedIsNull_WhenProxySourceIsNull()
        {
            var proxy = new GameObject("Proxy").AddComponent<MonoEntityProxy>();
            proxy.Source = null;

            var result = MonoEntity.TryCast(proxy, out MonoEntity casted);
            Assert.IsTrue(result);  // TryCast matches the proxy type
            Assert.IsNull(casted);  // But Source was null
        }

        [Test]
        public void TryCast_Generic_ReturnsFalse_IfSceneEntityIsWrongType()
        {
            var instance = new GameObject("Instance")
                .AddComponent<OtherMonoEntityDummy>();

            var result = MonoEntity.TryCast(instance, out AnotherMonoEntityDummy casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }

        [Test]
        public void TryCast_Generic_ReturnsFalse_IfProxyGenericTypeIsWrong()
        {
            var proxy = new GameObject("Proxy")
                .AddComponent<MonoEntityProxy<OtherMonoEntityDummy>>();

            var result = MonoEntity.TryCast(proxy, out AnotherMonoEntityDummy casted);

            Assert.IsFalse(result);
            Assert.IsNull(casted);
        }
        
        
        [Test]
        public void TryCast_ReturnsTrue_WhenDirectInstanceOfSceneEntity()
        {
            IEntity entity = new GameObject("MySceneEntity").AddComponent<MonoEntityDummy>();

            bool result = MonoEntity.TryCast(entity, out MonoEntity casted);

            Assert.IsTrue(result);
            Assert.AreEqual(entity, casted);
        }

        [Test]
        public void TryCast_Generic_ReturnsTrue_WhenCorrectGenericType()
        {
            IEntity entity = new GameObject("MySceneEntity")
                .AddComponent<MonoEntityDummy>();

            bool result = MonoEntity.TryCast(entity, out MonoEntityDummy casted);

            Assert.IsTrue(result);
            Assert.AreEqual(entity, casted);
        }

        [Test]
        public void TryCast_ReturnsTrue_WhenProxyMatchesGeneric()
        {
            var real = new GameObject("RealEntity").AddComponent<MonoEntityDummy>();
            var proxy = new GameObject("Proxy").AddComponent<MonoEntityProxyDummy>();
            proxy.Source = real;

            bool result = MonoEntity.TryCast(proxy, out MonoEntityDummy casted);

            Assert.IsTrue(result);
            Assert.AreEqual(real, casted);
        }

        #endregion
    }
}
using System;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed partial class SceneEntityTests
    {
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
    }
}
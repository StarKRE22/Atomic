using UnityEngine;

namespace Atomic.Entities
{
    public interface IGenericSceneEntityPool
    {
        SceneEntity Rent(SceneEntity prefab);
        SceneEntity Rent(SceneEntity prefab, Transform parent);
        SceneEntity Rent(SceneEntity prefab, Vector3 position, Quaternion rotation, Transform parent = null);
      
        void Return(IEntity entity);
        void Return(SceneEntity obj);
        void Clear(SceneEntity prefab);
    }
}
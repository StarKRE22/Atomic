using UnityEngine;

namespace Atomic.Entities
{
    public interface IScenePoolRegistry<E> where E : SceneEntity<E>
    {
        E Rent(E prefab);
        E Rent(E prefab, Transform parent);
        E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null);

        void Return(E entity);
        void Clear(E prefab);
        void Clear();
    }
}
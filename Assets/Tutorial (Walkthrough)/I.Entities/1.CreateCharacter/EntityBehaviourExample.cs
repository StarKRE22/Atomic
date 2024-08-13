using Atomic.Entities;
using UnityEngine;

namespace Walkthrough
{
    public sealed class EntityBehaviourExample : 
        IEntityInit, 
        IEntityEnable,
        IEntityDisable,
        IEntityDispose,
        IEntityUpdate,
        IEntityFixedUpdate,
        IEntityLateUpdate
    {
        
        //Calls like MonoBehaviour.Start()
        public void Init(IEntity entity)
        {
            Debug.Log($"Init {entity.Name}");

            GameObject gameObject = entity.GetValue<GameObject>(EntityAPI.GAME_OBJECT_KEY);
            Debug.Log($"GameObject active self: {gameObject.activeSelf}");
            
            Transform transform = entity.GetValue<Transform>(EntityAPI.TRANSFORM_KEY);
            Debug.Log($"Position: {transform.position}");

            int health = entity.GetValue<int>(EntityAPI.HEALTH_KEY);
            Debug.Log($"Health: {health}");

            float speed = entity.GetValue<int>(EntityAPI.SPEED_KEY);
            Debug.Log($"Speed: {speed}");
            
            Debug.Log($"Is Character: {entity.HasTag(EntityAPI.CHARACTER_TAG)}");
            Debug.Log($"Is Moveable: {entity.HasTag(EntityAPI.MOVEABLE_TAG)}");
            Debug.Log($"Is Coin: {entity.HasTag(EntityAPI.COIN_TAG)}");
        }

        //Calls like MonoBehaviour.Enable()
        public void Enable(IEntity entity)
        {
            Debug.Log($"Enable {entity.Name}");
        }

        //Calls like MonoBehaviour.Disable()
        public void Disable(IEntity entity)
        {
            Debug.Log($"Disable {entity.Name}");
        }

        //Calls like MonoBehaviour.OnDestroy()
        public void Dispose(IEntity entity)
        {
            Debug.Log($"Dispose {entity.Name}");
        }

        //Calls like MonoBehaviour.Update()
        public void OnUpdate(IEntity entity, float deltaTime)
        {
            Debug.Log($"Update {entity.Name}");
        }

        //Calls like MonoBehaviour.FixedUpdate()
        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            Debug.Log($"Fixed Update {entity.Name}");
        }

        //Calls like MonoBehaviour.LateUpdate()
        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            Debug.Log($"Late Update {entity.Name}");
        }
    }
}
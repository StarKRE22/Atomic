using Atomic.Contexts;
using UnityEngine;

namespace Walkthrough
{
    public sealed class ContextSystemExample : 
        IContextInit,
        IContextEnable,
        IContextDisable,
        IContextDispose,
        IContextUpdate,
        IContextFixedUpdate,
        IContextLateUpdate
    {
        public void Init(IContext context)
        {
            Debug.Log($"Init context {context.Name}");

            Transform worldTransform = context.GetValue<Transform>(GameContextAPI.WORLD_TRANSFORM);
            Debug.Log($"World transform: {worldTransform.name}");
            
            Transform poolTransform = context.GetValue<Transform>(GameContextAPI.POOL_TRANSFORM);
            Debug.Log($"Pool transform: {poolTransform.name}");

            int gameCountdown = context.GetValue<int>(GameContextAPI.GAME_COUNTDOWN);
            Debug.Log($"Game countdown: {gameCountdown}");

            Bounds spawnArea = context.GetValue<Bounds>(GameContextAPI.SPAWN_AREA);
            Debug.Log($"Spawn area: {spawnArea}");
        }

        //Calls like MonoBehaviour.Enable()
        public void Enable(IContext context)
        {
            Debug.Log($"Enable context: {context.Name}");
        }

        //Calls like MonoBehaviour.Disable()
        public void Disable(IContext context)
        {
            Debug.Log($"Disable context: {context.Name}");
        }

        //Calls like MonoBehaviour.OnDestroy()
        public void Dispose(IContext context)
        {
            Debug.Log($"Dispose context: {context.Name}");
        }

        public void Update(IContext context, float deltaTime)
        {
            Debug.Log($"Update context: {context.Name}");
        }

        public void FixedUpdate(IContext context, float deltaTime)
        {
            Debug.Log($"Fixed update context: {context.Name}");
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            Debug.Log($"Late update context: {context.Name}");
        }
    }
}
using Atomic.Contexts;
using UnityEngine;

namespace Walkthrough
{
    public sealed class GameContextInstallerExample : SceneContextInstallerBase
    {
        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform poolTransform;
        
        public override void Install(IContext context)
        {
            //Setup values:
            context.AddValue(GameContextAPI.WORLD_TRANSFORM, this.worldTransform);
            context.AddValue(GameContextAPI.POOL_TRANSFORM, this.poolTransform);
            context.AddValue(GameContextAPI.GAME_COUNTDOWN, 30);
            context.AddValue(GameContextAPI.SPAWN_AREA, new Bounds(Vector3.zero, new Vector3(5, 0, 5)));

            //Setup behaviours:
            context.AddSystem<ContextSystemExample>();
            
            Debug.Log("Game Context installed!");
        }
    }
}
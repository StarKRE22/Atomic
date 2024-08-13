using Atomic.Entities;
using UnityEngine;

namespace Walkthrough
{
    public sealed class CharacterInstallerExample : SceneEntityInstallerBase
    {
        public override void Install(IEntity entity)
        {
            //Setup values:
            entity.AddValue(EntityAPI.GAME_OBJECT_KEY, this.gameObject);
            entity.AddValue(EntityAPI.TRANSFORM_KEY, this.transform);
            entity.AddValue(EntityAPI.HEALTH_KEY, 3);
            entity.AddValue(EntityAPI.SPEED_KEY, 4);
            
            //Setup tags:
            entity.AddTag(EntityAPI.CHARACTER_TAG);
            entity.AddTag(EntityAPI.MOVEABLE_TAG);
            
            //Setup behaviours:
            entity.AddBehaviour<EntityBehaviourExample>();
            
            Debug.Log("Character installed!");
        }
    }
}
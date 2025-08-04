using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public static class EntitiesUseCase
    {
        public static GameEntity Spawn(
            IGameContext context,
            GameEntity prefab,
            Transform spawnPoint,
            TeamType team
        )
        {
            Transform worldTransform = context.GetWorldTransform();
            GameEntity entity = SceneEntity.Create(prefab, spawnPoint, worldTransform);
            entity.GetTeamType().Value = team;
            return entity;
        }
    }
}
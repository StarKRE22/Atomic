using System;
using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class CharacterUseCase
    {
        public static GameEntity Spawn(
            IPlayerContext context,
            IGameContext gameContext,
            GameEntity prefab
        )
        {
            Transform worldTransform = gameContext.GetWorldTransform();
            Transform spawnPoint = SpawnPointsUseCase.NextPoint(gameContext);
            GameEntity entity = SceneEntity.Create(prefab, spawnPoint, worldTransform);
            entity.GetTeamType().Value = context.GetTeamType().Value;
            return entity;
        }
        
        public static async UniTaskVoid RespawnWithDelay(IPlayerContext playerContext, IGameContext gameContext)
        {
            IValue<float> respawnTime = gameContext.GetRespawnDelay();
            await UniTask.Delay(TimeSpan.FromSeconds(respawnTime.Value));
            if (GameCycleUseCase.IsPlaying(gameContext)) 
                Respawn(playerContext, gameContext);
        }

        public static void Respawn(IPlayerContext playerContext, IGameContext gameContext)
        {
            IGameEntity character = playerContext.GetCharacter();
            character.GetHealth().AssignMax();
           
            Transform nextPoint = SpawnPointsUseCase.NextPoint(gameContext);
            character.GetPosition().Value = nextPoint.position;
            character.GetRotation().Value = nextPoint.rotation;
            
            character.GetRespawnEvent().Invoke();
            Debug.Log($"<color=green>Player {character.GetTeamType().Value} has respawned!</color>");
        }
    }
}
using System;
using System.Collections;
using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class CharacterUseCase
    {
        public static Actor Spawn(
            IPlayerContext context,
            IGameContext gameContext,
            Actor prefab
        )
        {
            Transform worldTransform = gameContext.GetWorldTransform();
            Transform spawnPoint = SpawnPointsUseCase.NextPoint(gameContext);
            Actor entity = SceneEntity.Create(prefab, spawnPoint, worldTransform);
            entity.GetTeamType().Value = context.GetTeamType().Value;
            return entity;
        }

        public static IEnumerator RespawnWithDelay(IPlayerContext playerContext, IGameContext gameContext)
        {
            IValue<float> respawnTime = gameContext.GetRespawnDelay();
            yield return new WaitForSeconds(respawnTime.Value);
            if (GameCycleUseCase.IsPlaying(gameContext))
                Respawn(playerContext, gameContext);
        }

        public static void Respawn(IPlayerContext playerContext, IGameContext gameContext)
        {
            IActor character = playerContext.GetCharacter();
            character.GetHealth().AssignMax();

            Transform nextPoint = SpawnPointsUseCase.NextPoint(gameContext);
            character.GetPosition().Value = nextPoint.position;
            character.GetRotation().Value = nextPoint.rotation;

            character.GetRespawnEvent().Invoke();
            DebugRespawn(character);
        }

        private static void DebugRespawn(IActor character)
        {
            TeamType teamType = character.GetTeamType().Value;
            Debug.Log($"<color={teamType.ToString().ToLower()}>Player {teamType} has respawned!</color>");
        }
    }
}
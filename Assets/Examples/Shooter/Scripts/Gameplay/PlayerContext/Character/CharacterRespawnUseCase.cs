// using System;
// using Atomic.Elements;
// using Atomic.Entities;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class CharacterRespawnUseCase
//     {
//         public static async UniTaskVoid RespawnWithDelay(IPlayerContext playerContext, IGameContext gameContext)
//         {
//             IValue<float> respawnTime = gameContext.GetRespawnTime();
//             await UniTask.Delay(TimeSpan.FromSeconds(respawnTime.Value));
//             Respawn(playerContext, gameContext);
//         }
//
//         public static void Respawn(in IPlayerContext playerContext, in IGameContext gameContext)
//         {
//             if (!GameCycleUseCase.IsPlaying(gameContext))
//                 return;
//             
//             Transform nextPoint = SpawnPointsUseCase.NextPoint(gameContext);
//             Vector3 position = nextPoint.position;
//             Quaternion rotation = nextPoint.rotation;
//             
//             IEntity character = playerContext.GetCharacter();
//             character.GetTransform().SetPositionAndRotation(position, rotation);
//             character.GetGameObject().SetActive(true);
//             character.GetHealth().AssignMax();
//             
//             Debug.Log($"<color=green>Player {character.GetTeam().Value} has respawned!</color>");
//         }
//     }
// }
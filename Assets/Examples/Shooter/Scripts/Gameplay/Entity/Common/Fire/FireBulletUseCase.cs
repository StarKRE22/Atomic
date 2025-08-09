// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class FireBulletUseCase
//     {
//         public static IEntity FireBullet(in IEntity entity, in IGameContext context)
//         {
//             Transform firePoint = entity.GetFirePoint();
//             Vector3 position = firePoint.position;
//             Quaternion rotation = firePoint.rotation;
//             
//             TeamType teamType = entity.GetTeam().Value;
//             return SpawnBulletUseCase.SpawnBullet(context, position, rotation, teamType);
//         }
//     }
// }
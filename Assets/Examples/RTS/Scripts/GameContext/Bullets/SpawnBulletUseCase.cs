// using Atomic.Entities;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class SpawnBulletUseCase
//     {
//         public static IEntity SpawnBullet(
//             in IGameContext context,
//             in Vector3 position,
//             in Quaternion rotation,
//             in IEntity owner,
//             in PlayerId playerId
//         )
//         {
//             IEntity bullet = context.GetBulletPool().Rent();
//             Transform bulletTransform = bullet.GetTransform();
//             bulletTransform.SetPositionAndRotation(position, rotation);
//
//             bullet.GetLifetime().Reset();
//             bullet.GetMoveDirection().Value = bulletTransform.forward;
//             bullet.GetPlayerId().Value = playerId;
//             bullet.GetOwner().Value = owner;
//             
//             return bullet;
//         }
//
//         public static void UnspawnBullet(in IGameContext context, in IEntity bullet)
//         {
//             context.GetBulletPool().Return(bullet);
//         }
//     }
// }
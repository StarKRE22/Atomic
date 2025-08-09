// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class SpawnBulletUseCase
//     {
//         public static IEntity SpawnBullet(
//             IGameContext context,
//             in Vector3 position,
//             in Quaternion rotation,
//             in TeamType teamType
//         )
//         {
//             IEntity bullet = context.GetBulletPool().Rent();
//             Transform bulletTransform = bullet.GetTransform();
//             bulletTransform.SetParent(context.GetWorldTransform());
//             bulletTransform.SetPositionAndRotation(position, rotation);
//             bullet.GetLifetime().Reset();
//             bullet.GetTeam().Value = teamType;
//             return bullet;
//         }
//
//         public static void UnspawnBullet(in IGameContext context, in IEntity bullet)
//         {
//             context.GetBulletPool().Return(bullet);
//         }
//     }
// }
using System;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class BulletUseCase
    {
        public static IEntity SpawnBullet(
            IGameContext context,
            in Vector3 position,
            in Quaternion rotation,
            in TeamType teamType
        )
        {
            // IEntity bullet = context.GetBulletPool().Rent();
            // Transform bulletTransform = bullet.GetTransform();
            // bulletTransform.SetParent(context.GetWorldTransform());
            // bulletTransform.SetPositionAndRotation(position, rotation);
            // bullet.GetLifetime().Reset();
            // bullet.GetTeam().Value = teamType;
            // return bullet;
            throw new NotImplementedException();
        }

        public static void DespawnBullet(in IGameContext context, in IEntity bullet)
        {
            throw new NotImplementedException();
            // context.GetBulletPool().Return(bullet);
        }
    }
}
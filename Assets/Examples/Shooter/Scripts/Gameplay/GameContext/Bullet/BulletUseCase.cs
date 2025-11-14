using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class BulletUseCase
    {
        public static IGameEntity Spawn(
            IGameContext context,
            Vector3 position,
            Quaternion rotation,
            TeamType teamType
        )
        {
            IEntityPool<IGameEntity> bulletPool = context.GetBulletPool();
            IGameEntity bullet = bulletPool.Rent();
            bullet.GetPosition().Value = position;
            bullet.GetRotation().Value = rotation;
            bullet.GetTeamType().Value = teamType;
            return bullet;
        }

        public static void Despawn(IGameContext context, IGameEntity bullet)
        {
            IEntityPool<IGameEntity> bulletPool = context.GetBulletPool();
            bulletPool.Return(bullet);
        }
    }
}
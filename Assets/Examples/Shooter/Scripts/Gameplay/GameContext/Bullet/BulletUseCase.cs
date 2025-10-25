using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class BulletUseCase
    {
        public static IEntity Spawn(
            IGameContext context,
            Vector3 position,
            Quaternion rotation,
            TeamType teamType
        )
        {
            IActor bullet = context.GetBulletPool().Rent();
            bullet.GetPosition().Value = position;
            bullet.GetRotation().Value = rotation;
            bullet.GetTeamType().Value = teamType;
            bullet.GetLifetime().ResetTime();
            return bullet;
        }

        public static void Despawn(IGameContext context, IActor bullet) => 
            context.GetBulletPool().Return(bullet);
    }
}
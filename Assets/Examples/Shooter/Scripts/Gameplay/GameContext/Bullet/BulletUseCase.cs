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
            IGameEntity bullet = context.GetBulletPool().Rent();
            bullet.GetPosition().Value = position;
            bullet.GetRotation().Value = rotation;
            bullet.GetTeamType().Value = teamType;
            bullet.GetLifetime().ResetTime();
            return bullet;
        }

        public static void Despawn(IGameContext context, IGameEntity bullet) => 
            context.GetBulletPool().Return(bullet);
    }
}
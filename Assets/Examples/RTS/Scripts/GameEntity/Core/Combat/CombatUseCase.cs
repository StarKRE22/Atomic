using UnityEngine;

namespace RTSGame
{
    public static class CombatUseCase
    {
        public static IGameEntity FireProjectile(
            IGameEntity source,
            string projectileName,
            IGameEntity target,
            IGameContext context
        )
        {
            IGameEntity projectile = FireProjectile(source, projectileName, context);
            projectile.GetTarget().Value = target;
            return projectile;
        }

        public static IGameEntity FireProjectile(IGameEntity source, string projectileName, IGameContext context) =>
            GameEntitiesUseCase.Spawn(
                context,
                projectileName,
                source.GetFirePoint().Value,
                source.GetRotation().Value,
                source.GetTeam().Value
            );

        public static Vector3 GetFirePoint(IGameEntity entity, Vector3 offset) => 
            entity.GetPosition().Value + entity.GetRotation().Value * offset;
    }
}
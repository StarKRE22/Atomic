using UnityEngine;

namespace RTSGame
{
    public static class CombatUseCase
    {
        public static IUnitEntity FireProjectile(
            IUnitEntity source,
            string projectileName,
            IUnitEntity target,
            IGameContext context
        )
        {
            IUnitEntity projectile = FireProjectile(source, projectileName, context);
            projectile.GetTarget().Value = target;
            return projectile;
        }

        public static IUnitEntity FireProjectile(IUnitEntity source, string projectileName, IGameContext context) =>
            GameEntitiesUseCase.Spawn(
                context,
                projectileName,
                source.GetFirePoint().Value,
                source.GetRotation().Value,
                source.GetTeam().Value
            );

        public static Vector3 GetFirePoint(IUnitEntity entity, Vector3 offset) => 
            entity.GetPosition().Value + entity.GetRotation().Value * offset;
    }
}
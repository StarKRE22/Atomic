using UnityEngine;

namespace RTSGame
{
    public static class CombatUseCase
    {
        public static IUnit FireProjectile(
            IUnit source,
            string projectileName,
            IUnit target,
            IGameContext context
        )
        {
            IUnit projectile = FireProjectile(source, projectileName, context);
            projectile.GetTarget().Value = target;
            return projectile;
        }

        public static IUnit FireProjectile(IUnit source, string projectileName, IGameContext context) =>
            UnitsUseCase.Spawn(
                context,
                projectileName,
                source.GetFirePoint().Value,
                source.GetRotation().Value,
                source.GetTeam().Value
            );

        public static Vector3 GetFirePoint(IUnit entity, Vector3 offset) => 
            entity.GetPosition().Value + entity.GetRotation().Value * offset;
    }
}
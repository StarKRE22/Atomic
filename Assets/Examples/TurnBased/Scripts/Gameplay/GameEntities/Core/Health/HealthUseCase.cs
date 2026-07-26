using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public static class HealthUseCase
    {
        public static bool HealthExists(this IGameEntity entity)
        {
            return entity.GetValue(GameEntityAPI.Health).Value > 0;
        }

        public static bool AnyAlive(this IEnumerable<IGameEntity> entities)
        {
            foreach (IGameEntity entity in entities)
                if (entity.HealthExists())
                    return true;

            return false;
        }

        public static bool ReduceHealth(this IGameEntity entity, int damage)
        {
            if (!entity.HealthExists())
                return false;

            IReactiveVariable<int> health = entity.GetValue(GameEntityAPI.Health);
            health.Value = Mathf.Max(0, health.Value - damage);
            return true;
        }

        public static void AssignMaxHealth(this IGameEntity entity)
        {
            entity.GetValue(GameEntityAPI.Health).Value = entity.GetValue(GameEntityAPI.MaxHealth).Value;
        }

        public static bool AssignZeroHealth(this IGameEntity entity)
        {
            if (!entity.HealthExists())
                return false;

            entity.GetValue(GameEntityAPI.Health).Value = 0;
            return true;
        }

        public static float GetHealthPercent(this IGameEntity entity) => 
            (float) entity.GetValue(GameEntityAPI.Health).Value / entity.GetValue(GameEntityAPI.MaxHealth).Value;
    }
}
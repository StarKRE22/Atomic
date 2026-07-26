using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class ProjectileMoveSystem : PriorityGameEntitySystem
    {
        private readonly IGameContext _gameContext;

        public ProjectileMoveSystem(
            IGameContext context,
            GamePriorityEntitySystemSettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasProjectileTag()),
                settings)
        {
            _gameContext = context;
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            IGameEntity target = entity.GetTarget().Value;
            if (target is not {Enabled: true})
            {
                entity.MoveStep(entity.GetForwardDirection(), deltaTime);
                return;
            }

            Vector3 vector = entity.GetDistanceVector(target);
            float scale = entity.GetScale().Value;
            if (vector.sqrMagnitude > scale * scale)
            {
                entity.GetRotation().Value = Quaternion.LookRotation(vector.normalized);
                entity.MoveStep(vector.normalized, deltaTime);
            }
            else if (entity.DealDamage(target)) 
                _gameContext.Despawn(entity);
        }
    }
}
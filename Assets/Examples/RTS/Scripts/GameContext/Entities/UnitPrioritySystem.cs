using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class UnitPrioritySystem : EntitySystem<IGameEntity>
    {
        private readonly UnitPrioritySettings _settings;
        private Transform _targetTransform;

        public UnitPrioritySystem(
            IGameContext context,
            UnitPrioritySettings settings)
            : base(
                new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasUnitTag()),
                settings)
        {
            _settings = settings;
        }

        protected override void Update(IGameEntity entity, float deltaTime)
        {
            if (_targetTransform == null)
                _targetTransform = Camera.main?.transform;

            if (_targetTransform == null)
                return;

            Vector3 entityPosition = entity.GetPosition().Value;
            float distance = Vector3.Magnitude(entityPosition - _targetTransform.position);

            entity.GetUpdatePriority().Value = distance <= _settings.highDistance
                ? EntityUpdatePriority.High
                : distance <= _settings.mediumDistance
                    ? EntityUpdatePriority.Medium
                    : EntityUpdatePriority.Low;
        }
    }
}

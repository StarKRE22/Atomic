using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class UnitPrioritySystem : FixedTickGameEntitySystem
    {
        [Header("Distance")]
        [SerializeField]
        private float _highDistance = 200;

        [SerializeField]
        private float _mediumDistance = 400;

        private Transform _targetTransform;

        protected override IReadOnlyEntityCollection<IGameEntity> ProvideEntityCollection(IGameContext context) => 
            new EntityFilter<IGameEntity>(context.GetEntityWorld(), e => e.HasUnitTag());
        
        protected override void OnInit(IGameContext context)
        {
            _targetTransform = Camera.main!.transform;
        }
        
        protected override void Update(IGameEntity entity, float deltaTime)
        {
            Vector3 entityPosition = entity.GetPosition().Value;
            float distance = Vector3.Magnitude(entityPosition - _targetTransform.position);

            entity.GetUpdatePriority().Value = distance <= _highDistance
                ? EntityUpdatePriority.High
                : distance <= _mediumDistance
                    ? EntityUpdatePriority.Medium
                    : EntityUpdatePriority.Low;
        }
    }
}
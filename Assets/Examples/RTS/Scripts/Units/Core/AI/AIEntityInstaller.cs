using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class AIEntityInstaller : IEntityInstaller<IUnit>
    {
        [SerializeField]
        private float _minDetectDuration = 0.2f;

        [SerializeField]
        private float _maxDetectDuration = 0.3f;

        [SerializeField]
        private Const<float> _detectionRadius = 30;
        
        public void Install(IUnit entity)
        {
            IGameContext gameContext = GameContext.Instance;
            entity.AddTarget(new ReactiveVariable<IUnit>());
            entity.AddDetectionRadius(_detectionRadius);
            entity.AddBehaviour(new AIDetectTargetBehaviour(
                new RandomCooldown(_minDetectDuration, _maxDetectDuration), gameContext)
            );
            entity.AddBehaviour<AIAttackTargetBehaviour>();

#if UNITY_EDITOR
            entity.AddBehaviour<DetectionRadiusGizmos>();
#endif
        }
    }
}
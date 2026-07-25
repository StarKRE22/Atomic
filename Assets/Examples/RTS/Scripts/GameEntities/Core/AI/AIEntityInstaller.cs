using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class AIEntityInstaller
    {
        [SerializeField]
        private float _minDetectDuration = 0.2f;

        [SerializeField]
        private float _maxDetectDuration = 0.3f;

        [SerializeField]
        private Const<float> _detectionRadius = 30;

        public void Install(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddTarget(new ReactiveVariable<IGameEntity>());
            entity.AddDetectionRadius(_detectionRadius);
            
            entity.AddDetectionCooldown(RandomCooldown
                .StartBuild()
                .WithMinDuration(_minDetectDuration)
                .WithMaxDuration(_maxDetectDuration)
                .WithRandomizer(UnityRandomizer.Instance)
                .Build()
            );

            // entity.AddBehaviour(new DetectTargetBehaviour(gameContext));
            // entity.AddBehaviour<AttackTargetBehaviour>();
        }
    }
}
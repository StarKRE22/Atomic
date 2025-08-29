using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class AIEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private float _minDetectDuration = 0.2f;

        [SerializeField]
        private float _maxDetectDuration = 0.3f;

        [SerializeField]
        private float _maxVision = 10;
        
        public void Install(IGameEntity entity)
        {
            IGameContext gameContext = GameContext.Instance;
            entity.AddTarget(new ReactiveVariable<IGameEntity>());
            entity.AddBehaviour(new DetectTargetBehaviour(
                new RandomCooldown(_minDetectDuration, _maxDetectDuration), gameContext, _maxVision)
            );
            entity.AddBehaviour<AttackTargetBehaviour>();
        }
    }
}
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class AIEntityInstaller : IEntityInstaller<IUnitEntity>
    {
        [SerializeField]
        private float _minDetectDuration = 0.2f;

        [SerializeField]
        private float _maxDetectDuration = 0.3f;
        
        public void Install(IUnitEntity entity)
        {
            IGameContext gameContext = GameContext.Instance;
            entity.AddTarget(new ReactiveVariable<IUnitEntity>());
            entity.AddBehaviour(new AIDetectTargetBehaviour(
                new RandomCooldown(_minDetectDuration, _maxDetectDuration), gameContext)
            );
            entity.AddBehaviour<AIAttackTargetBehaviour>();
        }
    }
}
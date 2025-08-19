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
        
        public void Install(IGameEntity entity)
        {
            IGameContext gameContext = EntryPoint.GameContext;
            entity.AddTarget(new ReactiveVariable<IGameEntity>());
            entity.AddBehaviour(new DetectTargetBehaviour(
                new RandomCooldown(_minDetectDuration, _maxDetectDuration), gameContext)
            );
            entity.AddBehaviour<AttackTargetBehaviour>();
        }
    }
}
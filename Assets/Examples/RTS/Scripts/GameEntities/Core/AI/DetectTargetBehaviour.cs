using Atomic.Elements;
using Atomic.Entities;
using Unity.Profiling;
using UnityEngine;
using UnityEditor;

namespace RTSGame
{
    public sealed class DetectTargetBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick, IEntityGizmos<IGameEntity>
    {
        private static readonly ProfilerMarker s_fixedTickMarker = new("DetectTargetBehaviour.FixedTickMarker");

        private readonly IGameContext _gameContext;

        private IEntityWorld<IGameEntity> _entityWorld;
        private IVariable<IGameEntity> _target;
        private IGameEntity _self;
        private ICooldown _cooldown;

        public DetectTargetBehaviour(IGameContext context)
        {
            _gameContext = context;
        }

        public void Init(IGameEntity entity)
        {
            _self = entity;
            _cooldown = entity.GetDetectionCooldown();
            _target = entity.GetTarget();
            _entityWorld = _gameContext.GetEntityWorld();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            using (s_fixedTickMarker.Auto())
            {
                _cooldown.Tick(deltaTime);

                if (_cooldown.IsCompleted() && !_entityWorld.Contains(_target.Value))
                {
                    _target.Value = _gameContext.FindClosestEnemy(_self);
                    _cooldown.ResetTime();
                }
            }
        }
        
        public void DrawGizmos(IGameEntity entity)
        {
            Vector3 center = entity.GetPosition().Value;
            float scale = entity.GetDetectionRadius().Value;
            Handles.color = Color.blueViolet;
            Handles.DrawWireDisc(center, Vector3.up, scale);
        }
    }
}
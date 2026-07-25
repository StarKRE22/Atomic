using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Unity.Profiling;

namespace RTSGame
{
    public sealed class ProjectileMoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
#if ENABLE_PROFILER
        private static readonly ProfilerMarker FixedTickMarker = new("ProjectileMove.FixedTick");
#endif

        private readonly IGameContext _gameContext;

        private IGameEntity _entity;
        private IValue<float> _scale;
        private IValue<IGameEntity> _target;

        public ProjectileMoveBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
            _scale = entity.GetScale();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
#if ENABLE_PROFILER
            using (FixedTickMarker.Auto())
#endif
            {
                IGameEntity target = _target.Value;

                // нет цели → просто летим вперёд
                if (target is not { Enabled: true })
                {
                    _entity.MoveStep(_entity.GetRotation().Value * Vector3.forward, deltaTime);
                    return;
                }

                Vector3 vector = _entity.GetDistanceVector(target);
                float scale = _scale.Value;
                float sqrScale = scale * scale;

                if (vector.sqrMagnitude > sqrScale)
                {
                    Vector3 dir = vector.normalized;
                    _entity.GetRotation().Value = Quaternion.LookRotation(dir);
                    _entity.MoveStep(dir, deltaTime);
                }
                else if (_entity.DealDamage(target)) 
                    _gameContext.Despawn(_entity);
            }
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Unity.Profiling;

namespace RTSGame
{
    public sealed class MoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick<IGameEntity>
    {
#if ENABLE_PROFILER
        private static readonly ProfilerMarker FixedTickMarker = new("MoveBehaviour.FixedTick");
#endif

        private IRequest<Vector3> _request;
        private ICommand<Vector3, float> _command;

        public void Init(IGameEntity entity)
        {
            _request = entity.GetMoveRequest();
            _command = entity.GetMoveCommand();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
#if ENABLE_PROFILER
            using (FixedTickMarker.Auto())
#endif
            {
                if (_request.Consume(out Vector3 direction))
                    _command.Invoke(direction, deltaTime);
            }
        }
    }
}
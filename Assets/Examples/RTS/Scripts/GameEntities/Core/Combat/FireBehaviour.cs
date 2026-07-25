using Atomic.Elements;
using Atomic.Entities;
using Unity.Profiling;

namespace RTSGame
{
    public sealed class FireBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick<IGameEntity>
    {
#if ENABLE_PROFILER
        private static readonly ProfilerMarker FixedTickMarker = new("FireBehaviour.FixedTick");
#endif

        private IRequest<IGameEntity> _request;
        private ICommand<IGameEntity> _command;

        public void Init(IGameEntity entity)
        {
            _request = entity.GetFireRequest();
            _command = entity.GetFireCommand();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
#if ENABLE_PROFILER
            using (FixedTickMarker.Auto())
#endif
            {
                if (_request.Consume(out IGameEntity target)) 
                    _command.Invoke(target);
            }
        }
    }
}
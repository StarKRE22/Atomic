using System;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class TeamEntityTrigger : DisposableEntityTrigger<IGameEntity>
    {
        protected override IDisposable Track(IGameEntity entity, Action<IGameEntity> callback) => 
            entity.GetTeam().Subscribe(_ => callback.Invoke(entity));
    }
}
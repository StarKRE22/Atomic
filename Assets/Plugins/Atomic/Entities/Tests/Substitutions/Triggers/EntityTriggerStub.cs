using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public class EntityTriggerStub : IEntityTrigger<IEntity>
    {
        public Action<IEntity> Action;
        public bool SetActionCalled;

        public readonly HashSet<IEntity> Tracked = new();

        public void SetAction(Action<IEntity> action)
        {
            SetActionCalled = true;
            Action = action;
        }

        public void Track(IEntity entity) => Tracked.Add(entity);

        public void Untrack(IEntity entity) => Tracked.Remove(entity);
    }
}
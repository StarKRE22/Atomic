using System;
using Atomic.Entities;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityAction_EnableSelf : IEntityAction
    {
        public void Invoke(IEntity entity)
        {
            entity.Enable();
        }
    }
}
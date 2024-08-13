using System;
using Atomic.Entities;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityAction_DisableSelf : IEntityAction
    {
        public void Invoke(IEntity entity)
        {
            entity.Disable();
        }
    }
}
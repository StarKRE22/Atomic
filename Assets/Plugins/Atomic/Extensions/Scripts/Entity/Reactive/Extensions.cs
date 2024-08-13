using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Entities;
using Action = System.Action;

namespace Atomic.Extensions
{
    public static partial class Extensions
    {
        public static void SubscribeAllBy<T>(
            this IReactive<T> targetEvent,
            IReadOnlyCollection<IEntityActionAsset> actions,
            IEntity entity
        )
        {
            if (actions == null)
            {
                return;
            }

            int count = actions.Count;
            if (count == 0)
            {
                return;
            }

            foreach (IEntityActionAsset creator in actions)
            {
                if (creator != null)
                {
                    Action action = creator.Create(entity);
                    targetEvent.Subscribe(_ => action.Invoke());
                }
            }
        }

        public static void SubscribeAllBy<T>(
            this IReactive<T> targetEvent,
            IEnumerable<IEntityActionAsset<T>> actions,
            IEntity entity
        )
        {
            if (actions == null)
            {
                return;
            }

            foreach (var creator in actions)
            {
                if (creator != null)
                {
                    var action = creator.Create(entity);
                    targetEvent.Subscribe(action);
                }
            }
        }

        public static void SubscribeAllBy(
            this IReactive targetEvent,
            IEntityActionAsset[] actions,
            IEntity entity
        )
        {
            if (actions == null)
            {
                return;
            }

            int count = actions.Length;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                IEntityActionAsset creator = actions[i];
                if (creator != null)
                {
                    Action action = creator.Create(entity);
                    targetEvent.Subscribe(action);
                }
            }
        }
    }
}
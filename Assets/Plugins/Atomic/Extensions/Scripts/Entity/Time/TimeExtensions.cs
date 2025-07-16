using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static class TimeExtensions
    {
        public static void SubscribeOnCompleteBy(
            this IExpiredSource expiredProvider,
            in IEnumerable<IEntityActionAsset> actions,
            in IEntity entity
        )
        {
            if (actions == null)
            {
                return;
            }

            foreach (IEntityActionAsset asset in actions)
            {
                expiredProvider.OnExpired += asset.Create(entity);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static partial class Extensions
    {
        public static void AppendBy(
            this IExpression<bool> expression,
            IEntityConditionAsset[] conditions,
            IEntity obj
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Length;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                IEntityConditionAsset creator = conditions[i];
                if (creator != null)
                {
                    Func<bool> condition = creator.Create(obj);
                    expression.Append(condition);
                }
            }
        }

        public static void AppendBy<T>(
            this IExpression<T, bool> expression,
            IReadOnlyCollection<IEntityConditionAsset<T>> conditions,
            IEntity entity
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Count;
            if (count == 0)
            {
                return;
            }

            foreach (var creator in conditions)
            {
                if (creator != null)
                {
                    Func<T, bool> condition = creator.Create(entity);
                    expression.Append(condition);
                }
            }
        }

        public static void AppendBy<T>(
            this IExpression<T, bool> expression,
            IReadOnlyCollection<IEntityConditionAsset> conditions,
            IEntity obj
        )
        {
            if (conditions == null)
            {
                return;
            }

            int count = conditions.Count;

            if (count == 0)
            {
                return;
            }

            foreach (var creator in conditions)
            {
                if (creator != null)
                {
                    Func<bool> condition = creator.Create(obj);
                    expression.Append(_ => condition.Invoke());
                }
            }
        }
    }
}
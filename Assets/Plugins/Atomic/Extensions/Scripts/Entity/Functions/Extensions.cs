using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static partial class Extensions
    {
        public static void AppendBy<T, R>(
            this IExpression<T, R> expression,
            IEnumerable<IEntityFunctionAsset<T, R>> creators,
            IEntity entity
        )
        {
            if (creators == null || !creators.Any())
            {
                return;
            }

            foreach (IEntityFunctionAsset<T, R> creator in creators)
            {
                if (creator != null)
                {
                    Func<T, R> func = creator.Create(entity);
                    expression.Append(func);
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public static partial class Extensions
    {
        public static void AppendBy(
            this IExpression<IEntity, bool> expression,
            in IEnumerable<IEntityPredicate_Entity> predicates,
            IEntity entity
        )
        {
            if (predicates == null)
            {
                return;
            }

            foreach (IEntityPredicate_Entity predicate in predicates)
            {
                if (predicate != null)
                {
                    expression.Append(other => predicate.Invoke(entity, other));
                }
            }
        }
        
        public static void AppendBy(
            this IExpression<IEntity, bool> expression,
            in IEnumerable<IEntityPredicate> predicates,
            IEntity entity
        )
        {
            if (predicates == null)
            {
                return;
            }

            foreach (IEntityPredicate predicate in predicates)
            {
                if (predicate != null)
                {
                    expression.Append(_ => predicate.Invoke(entity));
                }
            }
        }
        
        public static void AppendBy(
            this IExpression<bool> expression,
            in IEnumerable<IEntityPredicate> predicates,
            IEntity entity
        )
        {
            if (predicates == null)
            {
                return;
            }

            foreach (IEntityPredicate predicate in predicates)
            {
                if (predicate != null)
                {
                    expression.Append(() => predicate.Invoke(entity));
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IEnumerable<IEntityPredicate> predicates, IEntity target)
        {
            if (predicates == null)
            {
                return false;
            }

            foreach (IEntityPredicate predicate in predicates)
            {
                if (predicate == null)
                {
                    continue;
                }

                if (!predicate.Invoke(target))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
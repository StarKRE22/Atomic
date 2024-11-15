using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class OrExpression : ExpressionBase<bool>, IPredicate
    {
        public OrExpression()
        {
        }

        public OrExpression(IEnumerable<Func<bool>> members) : base(members)
        {
        }
        
        public OrExpression(params Func<bool>[] members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Invoke())
                {
                    return true;
                }
            }

            return false;
        }
    }

    [Serializable]
    public class OrExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
    {
        public OrExpression()
        {
        }

        public OrExpression(params Func<T, bool>[] members) : base(members)
        {
        }
        
        public OrExpression(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Invoke(args))
                {
                    return true;
                }
            }

            return false;
        }
    }
    
    [Serializable]
    public class OrExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
    {
        public OrExpression()
        {
        }

        public OrExpression(params Func<T1, T2, bool>[] members) : base(members)
        {
        }
        
        public OrExpression(IEnumerable<Func<T1, T2, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T1, T2, bool>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            if (count == 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (members[i].Invoke(arg1, arg2))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class AndExpression : ExpressionBase<bool>, IPredicate
    {
        public AndExpression()
        {
        }

        public AndExpression(params Func<bool>[] members) : base(members)
        {
        }

        public AndExpression(IEnumerable<Func<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    
    [Serializable]
    public class AndExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
    {
        public AndExpression()
        {
        }

        public AndExpression(params Func<T, bool>[] members) : base(members)
        {
        }

        public AndExpression(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Invoke(args))
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    [Serializable]
    public class AndExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
    {
        public AndExpression()
        {
        }

        public AndExpression(params Func<T1, T2, bool>[] members) : base(members)
        {
        }

        public AndExpression(IEnumerable<Func<T1, T2, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<Func<T1, T2, bool>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            
            if (count == 0)
            {
                return true;
            }
            
            for (int i = 0; i < count; i++)
            {
                if (!members[i].Invoke(arg1, arg2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
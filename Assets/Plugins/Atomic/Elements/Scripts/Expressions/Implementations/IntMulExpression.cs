using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    ///Represents a product of float members 

    [Serializable]
    public class IntMulExpression : ExpressionBase<int>
    {
        public IntMulExpression()
        {
        }

        public IntMulExpression(params Func<int>[] members) : base(members)
        {
        }
        
        public IntMulExpression(IEnumerable<Func<int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            int result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<int> member = members[i];
                result *= member.Invoke();
            }

            return result;
        }
    }
    
    [Serializable]
    public class IntMulExpression<T> : ExpressionBase<T, int>
    {
        public IntMulExpression()
        {
        }

        public IntMulExpression(params Func<T, int>[] members) : base(members)
        {
        }
        
        public IntMulExpression(IEnumerable<Func<T, int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<T, int>> members, T arg)
        {
            int result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }
            
            for (int i = 0; i < count; i++)
            {
                Func<T, int> member = members[i];
                result *= member.Invoke(arg);
            }

            return result;
        }
    }
    
    [Serializable]
    public class IntMulExpression<T1, T2> : ExpressionBase<T1, T2, int>
    {
        public IntMulExpression()
        {
        }

        public IntMulExpression(params Func<T1, T2, int>[] members) : base(members)
        {
        }
        
        public IntMulExpression(IEnumerable<Func<T1, T2, int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<T1, T2, int>> members, T1 arg1, T2 arg2)
        {
            int result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }
            
            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, int> member = members[i];
                result *= member.Invoke(arg1, arg2);
            }

            return result;
        }
    }
}

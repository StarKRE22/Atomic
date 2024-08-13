using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class IntSumExpression : ExpressionBase<int>
    {
        public IntSumExpression()
        {
        }
        
        public IntSumExpression(params Func<int>[] members) : base(members)
        {
        }

        public IntSumExpression(IEnumerable<Func<int>> members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 0;
            
            for (int i = 0; i < count; i++)
            {
                Func<int> member = members[i];
                result += member.Invoke();
            }

            return result;
        }
    }
    
    [Serializable]
    public class IntSumExpression<T> : ExpressionBase<T, int>
    {
        public IntSumExpression()
        {
        }

        public IntSumExpression(IEnumerable<Func<T, int>> members) : base(members)
        {
        }
        
        public IntSumExpression(params Func<T, int>[] members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<T, int>> members, T arg)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 0;
            
            for (int i = 0; i < count; i++)
            {
                Func<T, int> member = members[i];
                result += member.Invoke(arg);
            }

            return result;
        }
    }
    
    [Serializable]
    public class IntSumExpression<T1, T2> : ExpressionBase<T1, T2, int>
    {
        public IntSumExpression()
        {
        }

        public IntSumExpression(IEnumerable<Func<T1, T2, int>> members) : base(members)
        {
        }
        
        public IntSumExpression(params Func<T1, T2, int>[] members) : base(members)
        {
        }
        
        protected override int Invoke(IReadOnlyList<Func<T1, T2, int>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }
            
            int result = 0;
            
            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, int> member = members[i];
                result += member.Invoke(arg1, arg2);
            }

            return result;
        }
    }
}
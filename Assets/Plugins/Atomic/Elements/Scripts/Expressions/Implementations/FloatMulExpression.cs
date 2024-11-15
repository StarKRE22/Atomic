using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    ///Represents a product of float members 
    
    [Serializable]
    public class FloatMulExpression : ExpressionBase<float>
    {
        public FloatMulExpression()
        {
        }

        public FloatMulExpression(params Func<float>[] members) : base(members)
        {
        }

        public FloatMulExpression(IEnumerable<Func<float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            float result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<float> member = members[i];
                result *= member.Invoke();
            }

            return result;
        }
    }
    
    
    [Serializable]
    public class FloatMulExpression<T> : ExpressionBase<T, float>
    {
        public FloatMulExpression()
        {
        }

        public FloatMulExpression(params Func<T, float>[] members) : base(members)
        {
        }

        public FloatMulExpression(IEnumerable<Func<T, float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<T, float>> members, T arg)
        {
            float result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T, float> member = members[i];
                result *= member.Invoke(arg);
            }

            return result;
        }
    }
    
    [Serializable]
    public class FloatMulExpression<T1, T2> : ExpressionBase<T1, T2, float>
    {
        public FloatMulExpression()
        {
        }

        public FloatMulExpression(params Func<T1, T2, float>[] members) : base(members)
        {
        }

        public FloatMulExpression(IEnumerable<Func<T1, T2, float>> members) : base(members)
        {
        }
        
        protected override float Invoke(IReadOnlyList<Func<T1, T2, float>> members, T1 arg1, T2 arg2)
        {
            float result = 1;

            int count = members.Count;
            if (count == 0)
            {
                return result;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, float> member = members[i];
                result *= member.Invoke(arg1, arg2);
            }

            return result;
        }
    }
}

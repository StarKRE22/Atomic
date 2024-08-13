using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class FloatSumExpression : ExpressionBase<float>
    {
        public FloatSumExpression()
        {
        }

        public FloatSumExpression(params Func<float>[] members) : base(members)
        {
        }

        public FloatSumExpression(IEnumerable<Func<float>> members) : base(members)
        {
        }

        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }

            float result = 0;

            for (int i = 0; i < count; i++)
            {
                Func<float> member = members[i];
                result += member.Invoke();
            }

            return result;
        }
    }

    [Serializable]
    public class FloatSumExpression<T> : ExpressionBase<T, float>
    {
        public FloatSumExpression()
        {
        }

        public FloatSumExpression(params Func<T, float>[] members) : base(members)
        {
        }

        public FloatSumExpression(IEnumerable<Func<T, float>> members) : base(members)
        {
        }

        protected override float Invoke(IReadOnlyList<Func<T, float>> members, T arg)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }

            float result = 0;

            for (int i = 0; i < count; i++)
            {
                Func<T, float> member = members[i];
                result += member.Invoke(arg);
            }

            return result;
        }
    }

    [Serializable]
    public class FloatSumExpression<T1, T2> : ExpressionBase<T1, T2, float>
    {
        public FloatSumExpression()
        {
        }

        public FloatSumExpression(params Func<T1, T2, float>[] members) : base(members)
        {
        }

        public FloatSumExpression(IEnumerable<Func<T1, T2, float>> members) : base(members)
        {
        }

        protected override float Invoke(IReadOnlyList<Func<T1, T2, float>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            if (count == 0)
            {
                return 0;
            }

            float result = 0;

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, float> member = members[i];
                result += member.Invoke(arg1, arg2);
            }

            return result;
        }
    }
}
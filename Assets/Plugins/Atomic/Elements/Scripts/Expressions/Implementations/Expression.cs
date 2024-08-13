using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    [Serializable]
    public class Expression<R> : ExpressionBase<R>
    {
        private readonly Func<IReadOnlyList<Func<R>>, R> function;

        public Expression(Func<IReadOnlyList<Func<R>>, R> function)
        {
            this.function = function;
        }

        public Expression(Func<IReadOnlyList<Func<R>>, R> function, params Func<R>[] members) : base(members)
        {
            this.function = function;
        }

        public Expression(Func<IReadOnlyList<Func<R>>, R> function, IEnumerable<Func<R>> members) : base(members)
        {
            this.function = function;
        }

        protected override R Invoke(IReadOnlyList<Func<R>> members)
        {
            R invoke = this.function.Invoke(members);
            return invoke;
        }
    }

    [Serializable]
    public class Expression<T, R> : ExpressionBase<T, R>
    {
        private readonly Func<IReadOnlyList<Func<T, R>>, T, R> function;

        public Expression(Func<IReadOnlyList<Func<T, R>>, T, R> function)
        {
            this.function = function;
        }

        public Expression(Func<IReadOnlyList<Func<T, R>>, T, R> function, params Func<T, R>[] members) : base(members)
        {
            this.function = function;
        }

        public Expression(Func<IReadOnlyList<Func<T, R>>, T, R> function, IEnumerable<Func<T, R>> members)
            : base(members)
        {
            this.function = function;
        }

        protected override R Invoke(IReadOnlyList<Func<T, R>> members, T arg)
        {
            R invoke = this.function.Invoke(members, arg);
            return invoke;
        }
    }

    [Serializable]
    public class Expression<T1, T2, R> : ExpressionBase<T1, T2, R>
    {
        private readonly Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function;

        public Expression(Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function)
        {
            this.function = function;
        }

        public Expression(
            Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function,
            params Func<T1, T2, R>[] members) :
            base(members)
        {
            this.function = function;
        }

        public Expression(
            Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function,
            IEnumerable<Func<T1, T2, R>> members)
            : base(members)
        {
            this.function = function;
        }

        protected override R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2)
        {
            R invoke = this.function.Invoke(members, arg1, arg2);
            return invoke;
        }
    }
}
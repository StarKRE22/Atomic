using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable PublicConstructorInAbstractClass

namespace Atomic.Elements
{
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<R> : IExpression<R>
    {
        private readonly List<Func<R>> members;

        public R Value
        {
            get { return this.Invoke(this.members); }
        }

        public int MemberCount
        {
            get { return this.members.Count; }
        }

        public ExpressionBase()
        {
            this.members = new List<Func<R>>();
        }

        public ExpressionBase(params Func<R>[] members)
        {
            this.members = new List<Func<R>>(members);
        }

        public ExpressionBase(IEnumerable<Func<R>> members)
        {
            this.members = new List<Func<R>>(members);
        }

        public void Append(Func<R> member)
        {
            if (member != null)
            {
                this.members.Add(member);
            }
        }

        public void Remove(Func<R> member)
        {
            if (member != null)
            {
                this.members.Remove(member);
            }
        }

        public bool Contains(Func<R> member)
        {
            return this.members.Contains(member);
        }

        public void Clear()
        {
            this.members.Clear();
        }

#if ODIN_INSPECTOR

        [Button]
#endif
        public R Invoke()
        {
            return this.Invoke(this.members);
        }

        protected abstract R Invoke(IReadOnlyList<Func<R>> members);
    }

    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<T, R> : IExpression<T, R>
    {
        private readonly List<Func<T, R>> members;

        public int MemberCount
        {
            get { return this.members.Count; }
        }

        public ExpressionBase()
        {
            this.members = new List<Func<T, R>>();
        }

        public ExpressionBase(params Func<T, R>[] members)
        {
            this.members = new List<Func<T, R>>(members);
        }

        public ExpressionBase(IEnumerable<Func<T, R>> members)
        {
            this.members = new List<Func<T, R>>(members);
        }

        public void Append(Func<T, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(Func<T, R> member)
        {
            this.members.Remove(member);
        }

        public bool Contains(Func<T, R> member)
        {
            return this.members.Contains(member);
        }

        public void Clear()
        {
            this.members.Clear();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args)
        {
            return this.Invoke(this.members, args);
        }

        protected abstract R Invoke(IReadOnlyList<Func<T, R>> members, T args);
    }


    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<T1, T2, R> : IExpression<T1, T2, R>
    {
        private readonly List<Func<T1, T2, R>> members;

        public int MemberCount => this.members.Count;

        public ExpressionBase()
        {
            this.members = new List<Func<T1, T2, R>>();
        }

        public ExpressionBase(params Func<T1, T2, R>[] members)
        {
            this.members = new List<Func<T1, T2, R>>(members);
        }

        public ExpressionBase(IEnumerable<Func<T1, T2, R>> members)
        {
            this.members = new List<Func<T1, T2, R>>(members);
        }
        
        public void Append(Func<T1, T2, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(Func<T1, T2, R> member)
        {
            this.members.Remove(member);
        }

        public bool Contains(Func<T1, T2, R> member)
        {
            return this.members.Contains(member);
        }

        public void Clear()
        {
            this.members.Clear();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2)
        {
            return this.Invoke(this.members, arg1, arg2);
        }

        protected abstract R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2);
    }
}
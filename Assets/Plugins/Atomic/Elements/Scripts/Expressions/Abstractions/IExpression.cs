using System;

namespace Atomic.Elements
{
    public interface IExpression<T> : IValue<T>
    {
        int MemberCount { get; }
        
        void Append(Func<T> member);
        void Remove(Func<T> member);
        bool Contains(Func<T> member);
        void Clear();
    }

    public interface IExpression<T, R> : IFunction<T, R>
    {
        int MemberCount { get; }
    
        void Append(Func<T, R> member);
        void Remove(Func<T, R> member);
        bool Contains(Func<T, R> member);
        void Clear();
    }
    
    public interface IExpression<T1, T2, R> : IFunction<T1, T2, R>
    {
        int MemberCount { get; }

        void Append(Func<T1, T2, R> member);
        void Remove(Func<T1, T2, R> member);
        bool Contains(Func<T1, T2, R> member);
        void Clear();
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    public class StackExpression<T> : IExpression<T>
    {
        public T Value => _conditions.Count > 0 ? _conditions[^1].Invoke() : _defaultValue;

        public int Count => _conditions.Count;

        private readonly List<Func<T>> _conditions = new();
        private readonly T _defaultValue;

        public StackExpression(T defaultValue = default) => _defaultValue = defaultValue;

        public void Append(Func<T> member) => _conditions.Add(member);

        public void Remove(Func<T> member) => _conditions.Remove(member);

        public bool Contains(Func<T> member) => _conditions.Contains(member);

        public void Clear() => _conditions.Clear();
    }
}
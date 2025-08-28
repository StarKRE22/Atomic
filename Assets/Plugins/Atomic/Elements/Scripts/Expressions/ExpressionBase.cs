using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{R}"/> that aggregates multiple parameterless functions returning values of type <typeparamref name="R"/>.
    /// Supports dynamic modification and invocation of its function members via a linked-chain-like structure.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<R> : IExpression<R>
    {
        private const int INITIAL_CAPACITY = 1;

        private struct Node
        {
            public Func<R> Function;
            public int Next;
        }

        private Node[] _nodes;
        private int _head;
        private int _tail;
        private int _freeIndex;
        private int _count;

        /// <summary>
        /// Gets the evaluated result of the expression by invoking all registered function members.
        /// </summary>
        public R Value => Invoke();

        /// <summary>
        /// Gets the number of function members currently contained in the expression.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Initializes a new empty instance of the <see cref="ExpressionBase{R}"/> class.
        /// </summary>
        protected ExpressionBase(int capacity = INITIAL_CAPACITY)
        {
            _nodes = new Node[Math.Max(capacity, INITIAL_CAPACITY)];
            _head = -1;
            _tail = -1;
            _freeIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions to initialize the expression with.</param>
        protected ExpressionBase(params Func<R>[] members) : this(members.Length)
        {
            for (int i = 0, count = members.Length; i < count; i++)
                this.AddLast(members[i]);
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An enumerable of functions to initialize the expression with.</param>
        protected ExpressionBase(IEnumerable<Func<R>> members) : this(members.Count())
        {
            foreach (Func<R> member in members)
                this.AddLast(member);
        }

        /// <summary>
        /// Adds a function member to the end of the chain.
        /// </summary>
        /// <param name="member">The function to add.</param>
        public void AddLast(Func<R> member)
        {
            if (member == null)
                return;

            if (_freeIndex == _nodes.Length)
            {
                int newCapacity = _nodes.Length * 2;
                if (newCapacity < 0) newCapacity = int.MaxValue;
                Array.Resize(ref _nodes, newCapacity);
            }

            _nodes[_freeIndex] = new Node
            {
                Function = member,
                Next = -1
            };

            if (_tail != -1)
                _nodes[_tail].Next = _freeIndex;

            _tail = _freeIndex;

            if (_head == -1)
                _head = _freeIndex;

            _freeIndex++;
            _count++;
        }

        /// <summary>
        /// Adds a function member to the beginning of the chain.
        /// </summary>
        /// <param name="member">The function to add.</param>
        public void AddFirst(Func<R> member)
        {
            if (member == null) return;

            if (_freeIndex == _nodes.Length)
            {
                int newCapacity = _nodes.Length * 2;
                if (newCapacity < 0) newCapacity = int.MaxValue;
                Array.Resize(ref _nodes, newCapacity);
            }

            _nodes[_freeIndex] = new Node
            {
                Function = member,
                Next = _head
            };

            _head = _freeIndex;

            if (_tail == -1)
                _tail = _freeIndex;

            _freeIndex++;
            _count++;
        }

        /// <summary>
        /// Inserts a function member at the specified index in the chain.
        /// </summary>
        /// <param name="member">The function to insert.</param>
        /// <param name="index">The zero-based index at which to insert the member.</param>
        public void Insert(Func<R> member, int index = 0)
        {
            if (member == null)
                return;

            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and Count.");

            if (_freeIndex == _nodes.Length)
            {
                int newCapacity = _nodes.Length * 2;
                if (newCapacity < 0) newCapacity = int.MaxValue;
                Array.Resize(ref _nodes, newCapacity);
            }

            int nodeIndex = _freeIndex;
            _nodes[nodeIndex] = new Node {Function = member, Next = -1};
            _freeIndex++;

            if (index == 0) // insert at head
            {
                _nodes[nodeIndex].Next = _head;
                _head = nodeIndex;
                if (_tail == -1)
                    _tail = nodeIndex;
            }
            else
            {
                // find node at index-1
                int prev = _head;
                for (int i = 0; i < index - 1; i++)
                    prev = _nodes[prev].Next;

                _nodes[nodeIndex].Next = _nodes[prev].Next;
                _nodes[prev].Next = nodeIndex;

                if (_nodes[nodeIndex].Next == -1) // new tail
                    _tail = nodeIndex;
            }

            _count++;
        }

        /// <summary>
        /// Removes a function member from the chain.
        /// </summary>
        /// <param name="member">The function to remove.</param>
        public void Remove(Func<R> member)
        {
            if (_head == -1 || member == null)
                return;

            int current = _head;
            int prev = -1;

            while (current != -1)
            {
                if (_nodes[current].Function == member)
                {
                    if (prev != -1)
                        _nodes[prev].Next = _nodes[current].Next;
                    else
                        _head = _nodes[current].Next;

                    if (current == _tail)
                        _tail = prev;

                    _nodes[current] = default;
                    _count--;
                    break;
                }

                prev = current;
                current = _nodes[current].Next;
            }
        }

        /// <summary>
        /// Removes the function member at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index must be within the bounds of the collection.");

            if (index == 0) // remove head
            {
                _head = _nodes[_head].Next;
                if (_head == -1) //list is empty
                    _tail = -1;
            }
            else
            {
                int prev = _head;
                for (int i = 0; i < index - 1; i++)
                    prev = _nodes[prev].Next;

                int toRemove = _nodes[prev].Next;
                _nodes[prev].Next = _nodes[toRemove].Next;

                if (_nodes[prev].Next == -1) //removed tail
                    _tail = prev;
            }

            _count--;
        }

        /// <summary>
        /// Determines whether the chain contains the specified function member.
        /// </summary>
        /// <param name="member">The function to check.</param>
        /// <returns>True if the member exists; otherwise, false.</returns>
        public bool Contains(Func<R> member)
        {
            if (member != null)
            {
                int current = _head;
                while (current != -1)
                {
                    if (_nodes[current].Function == member) return true;
                    current = _nodes[current].Next;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all function members from the expression.
        /// </summary>
        public void Clear()
        {
            _head = -1;
            _tail = -1;
            _freeIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Invokes all registered function members and evaluates the expression.
        /// </summary>
        /// <returns>The result of the evaluated expression.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke()
        {
            R value = this.InitialValue();
            if (_count > 0)
            {
                int current = _head;
                while (current != -1)
                {
                    ref readonly Node node = ref _nodes[current];
                    if (this.Reduce(node.Function, ref value))
                        current = node.Next;
                    else
                        break;
                }
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract R InitialValue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract bool Reduce(Func<R> member, ref R current);
    }
}

//     /// <summary>
//     /// Represents a base implementation of <see cref="IExpression{T, R}"/> that aggregates multiple single-parameter functions.
//     /// </summary>
//     /// <typeparam name="T">The input type of the functions.</typeparam>
//     /// <typeparam name="R">The return type of the expression.</typeparam>
//     [Serializable]
// #if ODIN_INSPECTOR
//     [InlineProperty]
// #endif
//     public abstract class ExpressionBase<T, R> : IExpression<T, R>
//     {
//         private readonly List<Func<T, R>> members;
//
//         /// <summary>
//         /// Gets the number of function members in the expression.
//         /// </summary>
//         public int Count => this.members.Count;
//
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="ExpressionBase{T,R}"/> class.
//         /// </summary>
//         protected ExpressionBase() => this.members = new List<Func<T, R>>();
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of functions to initialize the expression with.</param>
//         protected ExpressionBase(params Func<T, R>[] members) => this.members = new List<Func<T, R>>(members);
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">A collection of functions to initialize the expression with.</param>
//         protected ExpressionBase(IEnumerable<Func<T, R>> members) => this.members = new List<Func<T, R>>(members);
//
//         /// <inheritdoc/>
//         public void Add(Func<T, R> member)
//         {
//             if (member != null) this.members.Add(member);
//         }
//
//         /// <inheritdoc/>
//         public void Remove(Func<T, R> member)
//         {
//             if (member != null) this.members.Remove(member);
//         }
//
//         /// <inheritdoc/>
//         public bool Contains(Func<T, R> member) => this.members.Contains(member);
//
//         /// <inheritdoc/>
//         public void Clear() => this.members.Clear();
//
//         /// <summary>
//         /// Invokes the expression with the given input argument.
//         /// </summary>
//         /// <param name="args">The input argument to pass to all functions.</param>
//         /// <returns>The result of the evaluated expression.</returns>
// #if ODIN_INSPECTOR
//         [Button]
// #endif
//         public R Invoke(T args) => this.Invoke(this.members, args);
//
//         /// <summary>
//         /// Template method that evaluates the expression using the specified list of function members.
//         /// </summary>
//         /// <param name="members">The list of function members.</param>
//         /// <param name="args">The input argument to pass to each function.</param>
//         /// <returns>The result of the expression.</returns>
//         protected abstract R Invoke(IReadOnlyList<Func<T, R>> members, T args);
//     }
//
//     /// <summary>
//     /// Represents a base implementation of <see cref="IExpression{T1, T2, R}"/> that aggregates multiple binary functions.
//     /// </summary>
//     /// <typeparam name="T1">The first input type.</typeparam>
//     /// <typeparam name="T2">The second input type.</typeparam>
//     /// <typeparam name="R">The return type of the expression.</typeparam>
//     [Serializable]
// #if ODIN_INSPECTOR
//     [InlineProperty]
// #endif
//     public abstract class ExpressionBase<T1, T2, R> : IExpression<T1, T2, R>
//     {
//         private readonly List<Func<T1, T2, R>> members;
//
//         /// <summary>
//         /// Gets the number of function members in the expression.
//         /// </summary>
//         public int Count => this.members.Count;
//
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="ExpressionBase{T1,T2,R}"/> class.
//         /// </summary>
//         protected ExpressionBase() => this.members = new List<Func<T1, T2, R>>();
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of functions to initialize the expression with.</param>
//         protected ExpressionBase(params Func<T1, T2, R>[] members) =>
//             this.members = new List<Func<T1, T2, R>>(members);
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">A collection of functions to initialize the expression with.</param>
//         protected ExpressionBase(IEnumerable<Func<T1, T2, R>> members) =>
//             this.members = new List<Func<T1, T2, R>>(members);
//
//         /// <inheritdoc/>
//         public void Add(Func<T1, T2, R> member)
//         {
//             if (member != null) this.members.Add(member);
//         }
//
//         /// <inheritdoc/>
//         public void Remove(Func<T1, T2, R> member)
//         {
//             if (member != null) this.members.Remove(member);
//         }
//
//         /// <inheritdoc/>
//         public bool Contains(Func<T1, T2, R> member) => this.members.Contains(member);
//
//         /// <inheritdoc/>
//         public void Clear() => this.members.Clear();
//
//         /// <summary>
//         /// Invokes the expression using the given input arguments.
//         /// </summary>
//         /// <param name="arg1">The first argument to pass to the functions.</param>
//         /// <param name="arg2">The second argument to pass to the functions.</param>
//         /// <returns>The result of the evaluated expression.</returns>
// #if ODIN_INSPECTOR
//         [Button]
// #endif
//         public R Invoke(T1 arg1, T2 arg2) => this.Invoke(this.members, arg1, arg2);
//
//         /// <summary>
//         /// Template method that evaluates the expression using the specified list of function members and input arguments.
//         /// </summary>
//         /// <param name="members">The list of function members.</param>
//         /// <param name="arg1">The first input argument.</param>
//         /// <param name="arg2">The second input argument.</param>
//         /// <returns>The result of the expression.</returns>
//         protected abstract R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2);
//     }
}
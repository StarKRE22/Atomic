using System;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class BaseRequest : IRequest
    {
        public bool Required => _required;

        [SerializeField]
        private bool _required;

        public void Invoke()
        {
            _required = true;
        }

        public bool Consume()
        {
            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    [Serializable]
    public class BaseRequest<T> : IRequest<T>
    {
        public bool Required => _required;
        public T Arg => _arg;

        [SerializeField]
        private T _arg;

        [SerializeField]
        private bool _required;

        public void Invoke(T arg)
        {
            _required = true;
            _arg = arg;
        }

        public bool TryGet(out T arg)
        {
            arg = _arg;
            return _required;
        }

        public bool Consume(out T arg)
        {
            arg = _arg;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    [Serializable]
    public class BaseRequest<T1, T2> : IRequest<T1, T2>
    {
        public bool Required => _required;
        public T1 Arg1 => _arg1;
        public T2 Arg2 => _arg2;

        [SerializeField]
        private T1 _arg1;

        [SerializeField]
        private T2 _arg2;

        [SerializeField]
        private bool _required;

        public void Invoke(T1 arg1, T2 arg2)
        {
            _required = true;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public bool TryGet(out T1 arg1, out T2 arg2)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            return _required;
        }
        
        public bool Consume(out T1 arg1, out T2 arg2)
        {
            arg1 = _arg1;
            arg2 = _arg2;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }

    [Serializable]
    public class BaseRequest<T1, T2, T3> : IRequest<T1, T2, T3>
    {
        public bool Required => _required;
        public T1 Arg1 => _arg1;
        public T2 Arg2 => _arg2;
        public T3 Arg3 => _arg3;

        [SerializeField]
        private T1 _arg1;

        [SerializeField]
        private T2 _arg2;

        [SerializeField]
        private T3 _arg3;

        [SerializeField]
        private bool _required;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            _required = true;
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
        }

        public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;
            return _required;
        }

        public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg1 = _arg1;
            arg2 = _arg2;
            arg3 = _arg3;

            if (!_required)
                return false;

            _required = false;
            return true;
        }
    }
}
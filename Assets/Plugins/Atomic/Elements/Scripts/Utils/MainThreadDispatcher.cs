using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Atomic.Elements
{
    internal sealed class MainThreadDispatcher : MonoBehaviour
    {
        public interface IFlushable
        {
            void Flush();
        }

        private static MainThreadDispatcher _instance;

        private static readonly HashSet<IFlushable> _dirty = new(256);
        private static readonly List<IFlushable> _processing = new(256);

        private static readonly object _lock = new();

        private static int _mainThreadId;

        public static bool IsMainThread =>
            Thread.CurrentThread.ManagedThreadId == _mainThreadId;

        public static void MarkDirty(IFlushable flushable)
        {
            if (flushable == null)
                return;

            lock (_lock)
                _dirty.Add(flushable);
        }
        
        private void Update()
        {
            
            // Thread 3
            // Thread 2
         
            lock (_lock)  //O
            {
                if (_dirty.Count == 0)
                    return;

                _processing.AddRange(_dirty);
                _dirty.Clear();
            }
            
            // Thread 1

            for (int i = 0, count = _processing.Count; i < count; i++)
                _processing[i].Flush();

            _processing.Clear();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var go = new GameObject("[MainThreadDispatcher]");
            DontDestroyOnLoad(go);

            _instance = go.AddComponent<MainThreadDispatcher>();
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}
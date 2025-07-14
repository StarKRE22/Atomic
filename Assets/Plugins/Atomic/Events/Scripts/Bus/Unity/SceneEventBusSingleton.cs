using System;

namespace Atomic.Events
{
    public abstract class SceneEventBusSingleton<T> : SceneEventBus where T : SceneEventBus
    {
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

#if UNITY_2023_1_OR_NEWER
                _instance = FindFirstObjectByType<T>();
#else
                _instance = FindObjectOfType<T>();
#endif

                if (_instance == null)
                    throw new NullReferenceException($"Scene Event Bus Singleton of type {typeof(T).Name} is not found on scene!");

                return _instance;
            }
        }

        private static T _instance;
    }
}
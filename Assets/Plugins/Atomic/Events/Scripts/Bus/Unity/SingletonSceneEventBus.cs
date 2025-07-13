using System;

namespace Atomic.Events
{
    public abstract class SingletonSceneEventBus<T> : SceneEventBus  where T : SceneEventBus
    {
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<T>();
                
                if (_instance == null)
                    throw new NullReferenceException("Can't find Event bus on scene!");

                return _instance;
            }
        }

        private static T _instance;
    }
}
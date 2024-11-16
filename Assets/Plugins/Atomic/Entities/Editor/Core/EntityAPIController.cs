#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    internal static class EntityAPIController
    {
        private const float _syncPeriod = 1.5f;
        private static double _currentTime;
        
        static EntityAPIController()
        {
            _currentTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _currentTime > _syncPeriod)
            {
                EntityAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }
    }
}
#endif
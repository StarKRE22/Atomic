#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Contexts
{
    [InitializeOnLoad]
    internal static class ContextAPIController
    {
        private const float _syncPeriod = 1.5f;
        private static double _currentTime;
        
        static ContextAPIController()
        {
            _currentTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _currentTime > _syncPeriod)
            {
                ContextAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }
    }
}
#endif
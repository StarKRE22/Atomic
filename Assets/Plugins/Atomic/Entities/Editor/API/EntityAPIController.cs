#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    internal static class EntityAPIController
    {
        private static readonly EntityAPISettings _settings;
        private static double _currentTime;

        static EntityAPIController()
        {
            _settings = EntityAPISettings.Instance;
            _currentTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (!_settings.autoRefresh)
                return;

            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _currentTime >= _settings.autoRefreshPeriod)
            {
                EntityAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }
    }
}
#endif
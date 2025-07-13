#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Events
{
    [InitializeOnLoad]
    internal static class EventBusAPIController
    {
        private static readonly EventBusAPISettings _settings;
        private static double _currentTime;

        static EventBusAPIController()
        {
            _settings = EventBusAPISettings.Instance;
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
                EventAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }
    }
}
#endif
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    public static class EntityAPIController
    {
        private const float _syncPeriod = 2.5f;
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
                 Debug.Log("REFRESH CONTROLLER");
                 EntityAPIManager.Refresh();
                 _currentTime = currentTime;
             }
        }
    }
}
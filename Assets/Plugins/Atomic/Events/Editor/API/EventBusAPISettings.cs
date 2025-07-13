#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    public sealed class EventBusAPISettings : ScriptableObject
    {
        public static EventBusAPISettings Instance => GetOrCreate();
        
        private static EventBusAPISettings _instance;

        [SerializeField]
        public bool autoRefresh = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(autoRefresh))]
#endif
        [SerializeField]
        public float autoRefreshPeriod = 1.0f;

        private static EventBusAPISettings GetOrCreate()
        {
            if (_instance != null)
                return _instance;

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(EventBusAPISettings));
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EventBusAPISettings settings = AssetDatabase.LoadAssetAtPath<EventBusAPISettings>(assetPath);
                _instance = settings;
            }
            else
            {
                _instance = Create();
            }

            return _instance;
        }

        private static EventBusAPISettings Create()
        {
            const string path = "Assets/Plugins/Atomic/Events/Editor/API/EventBusAPISettings.asset";
            EventBusAPISettings settings = CreateInstance<EventBusAPISettings>();

            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
    }
}
#endif
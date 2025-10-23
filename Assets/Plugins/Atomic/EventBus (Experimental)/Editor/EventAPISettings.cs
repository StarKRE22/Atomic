#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    public sealed class EventAPISettings : ScriptableObject
    {
        public static EventAPISettings Instance => GetOrCreate();
        
        private static EventAPISettings _instance;

        [SerializeField]
        public bool autoRefresh = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(autoRefresh))]
#endif
        [SerializeField]
        public float autoRefreshPeriod = 1.0f;

        private static EventAPISettings GetOrCreate()
        {
            if (_instance != null)
                return _instance;

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(EventAPISettings));
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EventAPISettings settings = AssetDatabase.LoadAssetAtPath<EventAPISettings>(assetPath);
                _instance = settings;
            }
            else
            {
                _instance = Create();
            }

            return _instance;
        }

        private static EventAPISettings Create()
        {
            const string path = "Assets/Plugins/Atomic/Events/Editor/EventAPISettings.asset";
            EventAPISettings settings = CreateInstance<EventAPISettings>();

            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
    }
}
#endif
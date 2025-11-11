#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    internal sealed class EntityAPISettings : ScriptableObject
    {
        private const string DEFAULT_PATH = "Assets/Plugins/Atomic/Entities/Editor/EntityAPISettings.asset";

        public static EntityAPISettings Instance => GetOrCreate();
        
        private static EntityAPISettings _instance;

        [SerializeField]
        public bool autoRefresh = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(autoRefresh))]
#endif
        [SerializeField]
        public float autoRefreshPeriod = 1.0f;

        private static EntityAPISettings GetOrCreate()
        {
            if (_instance != null)
                return _instance;

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(EntityAPISettings));
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EntityAPISettings settings = AssetDatabase.LoadAssetAtPath<EntityAPISettings>(assetPath);
                _instance = settings;
            }
            else
            {
                _instance = Create();
            }

            return _instance;
        }

        private static EntityAPISettings Create()
        {
            EntityAPISettings settings = CreateInstance<EntityAPISettings>();

            AssetDatabase.CreateAsset(settings, DEFAULT_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
    }
}
#endif
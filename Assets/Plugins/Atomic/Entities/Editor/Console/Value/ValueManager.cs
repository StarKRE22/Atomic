#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public static class ValueManager
    {
        private static ValueConfig _config;

        public static ValueConfig CreateValueConfig()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create Entity Value Settings...", "EntityValueSettings", "asset",
                "Please enter a file name to save the asset to");

            // const string path = "Assets/Plugins/MonoObjects/Editor/ValueCatalog.asset";
            _config = ScriptableObject.CreateInstance<ValueConfig>();

            AssetDatabase.CreateAsset(_config, path);
            AssetDatabase.SaveAssets();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }

            return _config;
        }

        public static ValueConfig GetValueConfig()
        {
            if (_config != null)
            {
                return _config;
            }
        
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(ValueConfig));
            int count = guids.Length;
            if (count == 0)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                ValueConfig valueConfig = AssetDatabase.LoadAssetAtPath<ValueConfig>(assetPath);
                if (valueConfig.local)
                {
                    continue;
                }
                
                _config = valueConfig;
                return _config;
            }

            throw new Exception("Config is not found!");
        }
    }
}
#endif


// const string path = "Assets/Plugins/MonoObjects/Editor/ValueCatalog.asset";
// ValueCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<ValueCatalog>(path);
// return scriptableObject;
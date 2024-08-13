#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class TagManager
    {
        private static TagsConfig _configuration;
        
        public static TagsConfig GetTagConfig()
        {
            if (_configuration != null)
            {
                return _configuration;
            }

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(TagsConfig));
            if (guids.Length == 0)
            {
                return null;
            }

            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            _configuration = AssetDatabase.LoadAssetAtPath<TagsConfig>(assetPath);
            return _configuration;
        }

        public static TagsConfig CreateTagConfig()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create Entity Tag Settings", "EntityTagSettings", "asset",
                "Please enter a file name to save the asset to");

            _configuration = ScriptableObject.CreateInstance<TagsConfig>();

            AssetDatabase.CreateAsset(_configuration, path);
            AssetDatabase.SaveAssets();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }

            return _configuration;
        }
    }
}
#endif
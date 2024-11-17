#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contexts
{
    [InitializeOnLoad]
    public static class APICatalogService
    {
        private static APICatalog catalog;
        
        static APICatalogService()
        {
            debugUtils.ValueNameFormatter = ConvertIdToName;
        }
        
        internal static APICatalog CreateCatalog()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create Context Settings...", "ContextSettings", "asset",
                "Please enter a file name to save the asset to");

            catalog = ScriptableObject.CreateInstance<APICatalog>();

            AssetDatabase.CreateAsset(catalog, path);
            AssetDatabase.SaveAssets();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }

            return catalog;
        }

        internal static APICatalog GetCatalog()
        {
            if (catalog != null)
            {
                return catalog;
            }
        
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(APICatalog));
            int count = guids.Length;
            if (count == 0)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                APICatalog apiCatalog = AssetDatabase.LoadAssetAtPath<APICatalog>(assetPath);
                if (apiCatalog.Inactive)
                {
                    continue;
                }
                
                catalog = apiCatalog;
                return catalog;
            }

            return null;
        }

        private static string ConvertIdToName(int id)
        {
            APICatalog catalog = GetCatalog();
            
            if (catalog == null)
            {
                return id.ToString();
            }
            
            return catalog.GetFullItemNameById(id);
        }
    }
}
#endif

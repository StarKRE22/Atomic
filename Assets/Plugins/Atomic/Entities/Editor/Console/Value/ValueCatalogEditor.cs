#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Atomic.Entities
{
    [CustomEditor(typeof(ValueConfig))]
    internal sealed class ValueCatalogEditor :
#if ODIN_INSPECTOR
        OdinEditor
#else
        Editor
#endif
    {
        public override void OnInspectorGUI()
        {
            this.DrawCompileButton();
            GUILayout.Space(8);
            base.OnInspectorGUI();
        }

        private void DrawCompileButton()
        {
            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f);
            if (GUILayout.Button("Compile"))
            {
                this.CompileKeys();
            }

            GUI.color = prevColor;
        }

        private void CompileKeys()
        {
            UpdateValueIndexesAndGenerate();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        private static void UpdateValueIndexesAndGenerate()
        {
            var assets = AssetDatabase.FindAssets("t:" + nameof(ValueConfig));

            for (var i = 0; i < assets.Length; i++)
            {
                var guid = assets[i];
                var currentInnerIndex = 0;

                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var config = AssetDatabase.LoadAssetAtPath<ValueConfig>(assetPath);
                for (var j = 0; j < config.categories.Count; j++)
                {
                    var configCategory = config.categories[j];
                    for (var k = 0; k < configCategory.indexes.Count; k++)
                    {
                        var item = configCategory.indexes[k];
                        item.id = currentInnerIndex + 10000 * i;
                        currentInnerIndex++;
                    }
                }

                ValueAPIGenerator.Generate(config);
            }
        }

    }
}
#endif
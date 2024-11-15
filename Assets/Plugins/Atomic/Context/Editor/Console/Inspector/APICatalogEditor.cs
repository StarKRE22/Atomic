#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Atomic.Contexts
{
    [CustomEditor(typeof(APICatalog))]
    internal sealed class APICatalogEditor :
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
            APICatalog catalog = this.target as APICatalog;
            APIGenerator.Generate(catalog);
        }
    }
}
#endif
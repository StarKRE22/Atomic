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
            ValueConfig audioBank = this.target as ValueConfig;
            ValueAPIGenerator.Generate(audioBank);
        }
    }
}
#endif
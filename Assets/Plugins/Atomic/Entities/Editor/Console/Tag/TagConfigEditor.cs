#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    [CustomEditor(typeof(TagsConfig))]
    internal sealed class TagConfigEditor : Editor
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
            GUI.color = new Color(0f, 0.83f, 1f, 1);

            if (GUILayout.Button("Compile"))
            {
                TagAPIGenerator.Generate(this.target as TagsConfig);
            }

            GUI.color = prevColor;
        }
    }
}
#endif
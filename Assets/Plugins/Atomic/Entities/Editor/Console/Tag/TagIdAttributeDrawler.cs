#if UNITY_EDITOR && ODIN_INSPECTOR
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    [UsedImplicitly]
    public sealed class TagIdAttributeDrawler : OdinAttributeDrawer<EntityTagAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            TagsConfig catalog = TagManager.GetTagConfig();
            
            if (catalog == null)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("No type catalog in project", MessageType.Error);
                return;
            }

            if (catalog.items.Count <= 0)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("Type catalog is empty", MessageType.Error);
                return;
            }

            int typeIndex = this.ValueEntry.SmartValue;

            int index = catalog.IndexOfItem(typeIndex);
            if (index == -1)
            {
                index = 0;
            }

            Color prevColor = GUI.color;
            
            // GUI.color = new Color(0f, 0.83f, 1f, 1);
            // GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label?.text, GUILayout.Width(7 * label?.text?.Length ?? 0 + 10));

            // EditorGUILayout.LabelField("Id", GUILayout.Width(70));

            index = EditorGUILayout.Popup(GUIContent.none, index, catalog.GetItemTypesWithIds());
            this.ValueEntry.SmartValue = catalog.GetItem(index).id;
            EditorGUILayout.EndHorizontal();
   
            // GUIHelper.PopLabelWidth();
            GUI.color = prevColor;
        }
    }
}
#endif
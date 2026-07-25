#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class EntityInspectorTagAttributeDrawer : OdinAttributeDrawer<EntityInspectorTagAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var entityType = this.Attribute.entityType;
            IList<string> tags = EntityInspectorCache.GetTagKeys(entityType);
            if (tags == null || tags.Count == 0)
            {
                CallNextDrawer(label);
                return;
            }

            var property = this.ValueEntry;

            int index = tags.IndexOf(property.SmartValue);
            if (index < 0) index = 0;

            int newIndex = EditorGUILayout.Popup(label, index, ToArray(tags));

            if (newIndex >= 0 && newIndex < tags.Count) 
                property.SmartValue = tags[newIndex];
        }

        private static string[] ToArray(IList<string> list)
        {
            string[] array = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                array[i] = list[i];
            
            return array;
        }
    }
}
#endif
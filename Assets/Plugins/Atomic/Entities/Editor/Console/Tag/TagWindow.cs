#if UNITY_EDITOR
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    internal sealed class TagWindow : EditorWindow
    {
        private const string DEFAULT_TYPE_NAME = "Enter Type...";

        private TagsConfig catalog;

        private SerializedObject catalogSerialized;
        private SerializedProperty itemsSerialized;

        private Vector2 _scrollPosition;
        private string _newTypeName = DEFAULT_TYPE_NAME;
        
        private double currentTime;

        private void OnEnable()
        {
            this.currentTime = EditorApplication.timeSinceStartup;
            this.DrawTitle();
        }

        private void DrawTitle()
        {
            this.titleContent = new GUIContent("Entity Tags");
        }

        private void OnLostFocus()
        {
            TagAPIGenerator.Generate(this.catalog, false);
        }
        
        private void OnInspectorUpdate()
        {
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - this.currentTime > 1.5f)
            {
                TagAPIGenerator.Generate(this.catalog, false);
                this.currentTime = currentTime;
            }
        }

        private void OnGUI()
        {
            this.catalog = TagManager.GetTagConfig();

            GUILayout.Space(8);
            this.DrawHeader();

            GUILayout.Space(8);
            this.DrawHorizontalSeparator();

            if (this.catalog == null)
            {
                return;
            }

            GUILayout.Space(8);
            this.DrawErrors();

            // this.catalog.Sort();
            this.catalogSerialized = new SerializedObject(this.catalog);
            this.itemsSerialized = this.catalogSerialized.FindProperty("items");

            this.DrawTypeList();
            this.catalogSerialized.ApplyModifiedProperties();
        }

        private void DrawErrors()
        {
            if (this.catalog.HasDuplicatedId(out int index))
            {
                Color prevColor = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.HelpBox($"There is duplicate index {index}! Please, fix it in the Type Catalog!",
                    MessageType.Error);
                GUI.color = prevColor;
            }

            if (this.catalog.HasDuplicatedType(out string type))
            {
                Color prevColor = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.HelpBox($"There is duplicate type {type}! Please, fix it!", MessageType.Warning);
                GUI.color = prevColor;
            }
        }

        private void DrawHeader()
        {
            Rect rect = EditorGUILayout.BeginHorizontal();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20
            };

            rect.x += 10;
            GUI.Label(rect, "Entity Tags:", labelStyle);

            EditorGUILayout.Space(24);
            this.DrawCatalogButton();
            this.DrawCompileButton();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawHorizontalSeparator()
        {
            //Draw separator line:
            const float separatorWidth = 1;
            Color separatorColor = Color.gray;

            Rect rect = EditorGUILayout.GetControlRect();
            Rect separatorRect = new Rect(rect.x, rect.y, rect.width, separatorWidth);
            EditorGUI.DrawRect(separatorRect, separatorColor);
        }

        private void DrawTypeList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(8);
            
            EditorGUILayout.BeginVertical();

            _scrollPosition = EditorGUILayout.BeginScrollView(
                _scrollPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)
            );


            DrawFirstElement();
            this.DrawCustomElements();

            EditorGUILayout.Space(30);
            this.DrawAddElementButton();

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(8);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawFirstElement()
        {
            GUI.enabled = false;
            EditorGUILayout.TextField(new GUIContent("Index: 0"), "RESERVED");
            GUI.enabled = true;
        }

        private void DrawCustomElements()
        {
            for (int i = 0, count = this.itemsSerialized.arraySize; i < count; i++)
            {
                this.DrawCustomElement(i);
            }
        }

        private void DrawCustomElement(int itemIndex)
        {
            Color prevColor = GUI.color;

            SerializedProperty key = this.itemsSerialized.GetArrayElementAtIndex(itemIndex);
            SerializedProperty id = key.FindPropertyRelative(nameof(TagsConfig.Item.id));
            SerializedProperty type = key.FindPropertyRelative(nameof(TagsConfig.Item.type));

            EditorGUILayout.BeginHorizontal();

            bool isUniqueueType = this.catalog.IsUniqueueType(type.stringValue);
            if (!isUniqueueType)
            {
                GUI.color = Color.red;
            }

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
            if (!Regex.IsMatch(type.stringValue, namePattern))
            {
                GUI.color = Color.red;
            }

            bool isUniqueueId = this.catalog.IsUniqueueId(id.intValue);
            if (!isUniqueueId)
            {
                GUI.color = Color.red;
            }

            //Draw type field:
            GUIContent label = new GUIContent($"Index: {id.intValue}");
            type.stringValue = EditorGUILayout.TextField(label, type.stringValue, GUILayout.ExpandWidth(true));

            GUI.color = Color.red;

            //Draw "remove" button:
            if (GUILayout.Button("-", GUILayout.Width(50)))
            {
                this.catalog.RemoveItemAt(itemIndex);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }

            GUI.color = prevColor;

            EditorGUILayout.EndHorizontal();
        }

        private void DrawAddElementButton()
        {
            int freeId = this.catalog.GetFreeId();
            GUIContent label = new GUIContent($"Index: {freeId}");

            Color prevColor = GUI.color;

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

            if (Regex.IsMatch(_newTypeName, namePattern) && !this.catalog.TypeExists(_newTypeName))
            {
                // GUILayout.Label("Create New", new GUIStyle(GUI.skin.label));

                GUI.color = new Color(0f, 0.83f, 1f, 1);
                EditorGUILayout.BeginHorizontal();

                //Draw type field:
                // GUIStyle style = new GUIStyle(GUI.skin.textField)
                // {
                //     fontSize = 14,
                // };

                _newTypeName = EditorGUILayout.TextField(label, _newTypeName, GUILayout.Height(20));

                //Draw "add" button:
                if (GUILayout.Button("+", GUILayout.Width(50)))
                {
                    this.catalog.AddItem(freeId, _newTypeName);
                    _newTypeName = DEFAULT_TYPE_NAME;
                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                }

                EditorGUILayout.EndHorizontal();
                
                GUI.color = prevColor;
                EditorGUILayout.HelpBox("Create New Type", MessageType.Info);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUI.color = Color.yellow;

                //Draw type field:
                _newTypeName = EditorGUILayout.TextField(label, _newTypeName);

                GUI.enabled = false;

                //Draw "add" button:
                GUILayout.Button("+", GUILayout.Width(50));
                GUI.enabled = true;

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.HelpBox($"Invalid type name: '{_newTypeName}'", MessageType.Warning);
                GUI.color = prevColor;
            }
        }

        private void DrawCatalogButton()
        {
            if (this.catalog != null)
            {
                return;
            }

            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f, 1);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 15,
            };

            if (GUILayout.Button("Create Settings", buttonStyle, GUILayout.Height(30), GUILayout.MaxWidth(250)))
            {
                this.catalog = TagManager.CreateTagConfig();
            }

            GUI.color = prevColor;
        }

        private void DrawCompileButton()
        {
            if (this.catalog == null)
            {
                return;
            }

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

            if (this.catalog.HasDuplicatedId(out _) ||
                this.catalog.HasDuplicatedType(out _) ||
                !this.catalog.AllMatchesPattern(namePattern))
            {
                GUI.enabled = false;
            }

            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f, 1);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 15,
            };

            if (GUILayout.Button($"Compile >>> {this.catalog.className}.cs", buttonStyle, GUILayout.Height(30),
                    GUILayout.MaxWidth(250)))
            {
                TagAPIGenerator.Generate(this.catalog, true);
            }

            GUI.color = prevColor;
            GUI.enabled = true;
        }
    }
}
#endif
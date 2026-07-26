// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StylizedWater3
{
    public static class HeightQuerySystemEditor
    {
        public class HeightQueryInspector : EditorWindow
        {
            [MenuItem("Window/Analysis/Stylized Water 3/Height Query Inspector", false, 0)]
            public static void Open()
            {
                HeightQueryInspector window = GetWindow<HeightQueryInspector>(false);
                window.titleContent = new GUIContent("Height Query Inspector", EditorGUIUtility.IconContent("ComputeShader Icon").image);

                window.autoRepaintOnSceneChange = true;
                window.minSize = new Vector2(1200f, 200f);

                window.Show();
            }

            private Vector2 scrollPos;
            private Vector2 outputScrollPos;

            private int selectedQueryIndex;
            private int requestIndex;
            private int highlightedRequest = -1;

            //private bool showOutput = false;
            private bool showIndices = false;
            
            [SerializeField]
            private GUIStyle labelStyle;
            private GUIStyle labelStyleSelected;

            private Color GetUniqueColor(int i)
            {
                UnityEngine.Random.InitState(i);
                return Color.HSVToRGB(UnityEngine.Random.value, 0.75f, 1f);
            }

            private readonly Color lineColor = new Color(0,0,0, 0.5f);
            
            private void OnGUI()
            {
                labelStyle = new GUIStyle(EditorStyles.label);
                labelStyle.richText = true;
                labelStyleSelected = new GUIStyle(EditorStyles.selectionRect);
                labelStyleSelected.richText = true;
                labelStyleSelected.onFocused = new GUIStyleState();
                labelStyleSelected.onFocused.background = Texture2D.grayTexture;

                this.Repaint();

                var queryCount = HeightQuerySystem.QueryCount;

                if (queryCount == 0)
                {
                    EditorGUILayout.HelpBox("No water height queries are currently enqueued", MessageType.Info);
                    return;
                }
                
                //showOutput = EditorGUILayout.Toggle("Show output", showOutput);

                using (new EditorGUILayout.HorizontalScope(GUILayout.MaxWidth(200f)))
                {
                    EditorGUILayout.LabelField($"Query ({queryCount}):", EditorStyles.boldLabel, GUILayout.MaxWidth(100f));
                    GUIContent[] content = new GUIContent[queryCount];
                    for (int i = 0; i < content.Length; i++)
                    {
                        content[i] = new GUIContent(i.ToString());
                    }
                    selectedQueryIndex = GUILayout.Toolbar(selectedQueryIndex, content);
                }
                selectedQueryIndex = Mathf.Min(selectedQueryIndex, queryCount);
                
                int queryIndex = 0;
                foreach (HeightQuerySystem.Query query in HeightQuerySystem.queries)
                {
                    if (selectedQueryIndex == queryIndex)
                    {
                        EditorGUILayout.LabelField($"Capacity: {query.sampleCount}/{HeightQuerySystem.Query.MAX_SIZE}", EditorStyles.boldLabel);
                        Rect rect = EditorGUILayout.GetControlRect();
                        rect.width -= 5f;
                        
                        //EditorGUI.DrawRect(rect, Color.grey);
                        
                        float cellWidth = rect.width / (float)HeightQuerySystem.Query.MAX_SIZE;

                        //Draw available indices
                        for (int i = 0; i < query.availableIndices.Count; i++)
                        {
                            float x = rect.x + (float)(query.availableIndices[i] * cellWidth);
                            Rect cellRect = new Rect(x, rect.y, cellWidth, rect.height);
                            EditorGUI.DrawRect(cellRect, Color.grey * 0.6f);
                        }
                        
                        //Draw occupied indices
                        requestIndex = 0;
                        highlightedRequest = -1;
                        foreach (KeyValuePair<int, HeightQuerySystem.AsyncRequest> request in query.requests)
                        {
                            Color color = GetUniqueColor(request.Key);
                            for (int i = 0; i < request.Value.indices.Count; i++)
                            {
                                int sampleIndex = request.Value.indices[i];
                                float x = rect.x + (float)(sampleIndex * cellWidth);
                                
                                Rect cellRect = new Rect(x, rect.y, cellWidth, rect.height);
                                EditorGUI.DrawRect(cellRect, color);

                                if (cellRect.Contains(Event.current.mousePosition))
                                {
                                    highlightedRequest = requestIndex;
                                    
                                    GUIContent tooltip = new GUIContent($"[{sampleIndex.ToString()}] {request.Value.label}");
                                    
                                    Rect tooltipRect = cellRect;
                                    tooltipRect.y -= tooltipRect.height + 5f;
                                    tooltipRect.width = EditorStyles.label.CalcSize(tooltip).x;
                                    EditorGUI.HelpBox(tooltipRect, tooltip);
                                }
                            }
                            requestIndex++;
                        }

                        //Draw overlay lines
                        for (int i = 0; i < HeightQuerySystem.Query.MAX_SIZE; i++)
                        {
                            float x = rect.x + (float)(i * cellWidth);
                            Rect lineRect = new Rect(x, rect.y, 1f, rect.height);
                            EditorGUI.DrawRect(lineRect, lineColor);
                        }

                        showIndices = EditorGUILayout.ToggleLeft("Show available indices", showIndices);
                        if (showIndices)
                        {
                            using (new EditorGUILayout.HorizontalScope(EditorStyles.textArea))
                            {
                                string indicesString = "";
                                for (int i = 0; i < query.availableIndices.Count; i++)
                                {
                                    indicesString += $"{query.availableIndices[i].ToString()}, ";
                                }
                                
                                EditorGUILayout.LabelField(indicesString, EditorStyles.miniLabel);
                            }
                        }
                        EditorGUILayout.LabelField($"Requests ({query.requests.Count}):", EditorStyles.boldLabel);

                        using (new EditorGUILayout.VerticalScope(EditorStyles.textArea))
                        {
                            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true);
                            requestIndex = 0;
                            foreach (KeyValuePair<int, HeightQuerySystem.AsyncRequest> request in query.requests)
                            {
                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    Rect iconRect = EditorGUILayout.GetControlRect(GUILayout.MaxWidth(10f));
                                    iconRect.width = 10f;
                                    iconRect.height = 10f;
                                    iconRect.y += 5f;
                                    EditorGUI.DrawRect(iconRect, GetUniqueColor(request.Key));

                                    GUILayout.Space(5f);

                                    bool selected = highlightedRequest == requestIndex;
                                    
                                    EditorGUILayout.LabelField($"<b>Label:</b> {request.Value.label}", selected ? labelStyleSelected : labelStyle, GUILayout.Width(200f));
                                    EditorGUILayout.LabelField($"<b>ID:</b> {request.Value.hashCode}", selected ? labelStyleSelected : labelStyle, GUILayout.Width(75f));
                                    EditorGUILayout.LabelField($"<b>Samples:</b> {request.Value.SampleCount}", selected ? labelStyleSelected : labelStyle, GUILayout.Width(100f));

                                    string indicesString = "{ ";
                                    for (int i = 0; i < request.Value.indices.Count; i++)
                                    {
                                        indicesString += $"{request.Value.indices[i].ToString()}, ";
                                    }
                                    indicesString += " }";
                                    EditorGUILayout.LabelField($"<b>Indices:</b> {indicesString}", selected ? labelStyleSelected : labelStyle);
                                }
                                
                                /*
                                if (showOutput)
                                {
                                    EditorGUILayout.LabelField("Outputs");
                                    using (new EditorGUILayout.VerticalScope(EditorStyles.textArea, GUILayout.MaxHeight(200f)))
                                    {
                                        //outputScrollPos = EditorGUILayout.BeginScrollView(outputScrollPos);
                                        for (int i = 0; i < request.Value.indices.Count; i++)
                                        {
                                            int index = request.Value.indices[i];
                                            EditorGUILayout.LabelField(index + ": " + query.outputOffsets[index], EditorStyles.miniLabel);
                                        }
                                        //EditorGUILayout.EndScrollView();
                                    }
                                }
                                */

                                requestIndex++;
                            }
                            EditorGUILayout.EndScrollView();
                        }
                    }
                    queryIndex++;
                }
            }
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace StylizedWater3
{
    public class RenderTargetDebuggerWindow : EditorWindow
    {
        private const int m_width = 550;
        private const int m_height = 300;
        
        [MenuItem("Window/Analysis/Stylized Water 3/Render targets", false, 0)]
        private static void OpenDebugger()
        {
            RenderTargetDebuggerWindow.Open();
        }
        
        #if SWS_DEV
        [MenuItem("SWS/Debug/Render Targets")]
        #endif
        public static void Open()
        {
            RenderTargetDebuggerWindow window = GetWindow<RenderTargetDebuggerWindow>(false);
            window.titleContent = new GUIContent("Water Render Buffer Inspector");

            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(m_width, m_height);
            //window.maxSize = new Vector2(m_width, m_height);
            window.Show();
        }

        private float width = 300f;
        private Vector2 scrollPos;

        private ColorWriteMask colorMask = ColorWriteMask.All;
        private int colorChannel = 1;
        private int renderTargetIndex;
        private float exposure = 1f;
        
        private void OnEnable()
        {
            RenderTargetDebugger.Initialize();
        }

        private void OnDisable()
        {
            RenderTargetDebugger.Cleanup();
        }

        private void OnGUI()
        {
            Repaint();
            
            if (RenderTargetDebugger.renderTargetNames.Length < 5)
            {
                renderTargetIndex = GUILayout.Toolbar(renderTargetIndex, RenderTargetDebugger.renderTargetNames);
            }
            else
            {
                renderTargetIndex = EditorGUILayout.Popup($"Render target ({RenderTargetDebugger.renderTargets.Count})", renderTargetIndex, RenderTargetDebugger.renderTargetNames);
            }
            
            width = (Mathf.Min(this.position.height, this.position.width) * 1f) - 15f;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            //EditorGUILayout.LabelField($"RenderTargetInspector.InspectedProperty: {RenderTargetInspector.InspectedProperty}");
            //EditorGUILayout.LabelField($"RenderTargetInspector.CurrentRT: {RenderTargetInspector.CurrentRT}");
            
            int currentTarget = 0;
            foreach (var renderTarget in RenderTargetDebugger.renderTargets)
            {
                if(renderTargetIndex == currentTarget)
                {
                    //mark as current!
                    RenderTargetDebugger.InspectedProperty = renderTarget.propertyID;

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            DrawTexture(renderTarget);
                        }
                    }
                }

                currentTarget++;

            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawTexture(RenderTargetDebugger.RenderTarget renderTarget)
        {
            if (RenderTargetDebugger.CurrentRT == null || renderTarget == null)
            {
                EditorGUILayout.HelpBox($"Render target \"{renderTarget.textureName}\" couldn't be found, or it is not bound." +
                                        $"\n\nThe related render pass may be disabled, or not render for the current view (scene/game view not open)", MessageType.Info);
                return;
            }

            //Null at the very first frame
            if (RenderTargetDebugger.CurrentRT.rt == null) return;
            
            EditorGUILayout.LabelField($"\"{renderTarget.textureName}\" {RenderTargetDebugger.CurrentRT.rt.graphicsFormat} {RenderTargetDebugger.CurrentRT.rt.width}x{RenderTargetDebugger.CurrentRT.rt.height}px @ {(UnityEngine.Profiling.Profiler.GetRuntimeMemorySizeLong(RenderTargetDebugger.CurrentRT) / 1024f / 1024f).ToString("F2")}mb", EditorStyles.boldLabel);
            if (renderTarget.description != string.Empty) EditorGUILayout.HelpBox(renderTarget.description, MessageType.Info);
            
            Rect rect = EditorGUILayout.GetControlRect();

            Rect position = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
            position.width = width;

            //colorChannel = EditorGUI.Popup(position, "Channel mask", colorChannel, new string[] { "RGB", "R", "G", "B", "A" }); 
            colorChannel = (int)GUI.Toolbar(position, colorChannel, new GUIContent[] { new GUIContent("RGBA"), new GUIContent("RGB"), new GUIContent("R"), new GUIContent("G"), new GUIContent("B"), new GUIContent("A") });

            switch (colorChannel)
            {
                case 1: colorMask = ColorWriteMask.All;
                    break;
                case 2: colorMask = ColorWriteMask.Red;
                    break;
                case 3: colorMask = ColorWriteMask.Green;
                    break;
                case 4: colorMask = ColorWriteMask.Blue;
                    break;
                case 5: colorMask = ColorWriteMask.Alpha;
                    break;
            }

            rect.y += 21f;
            rect.width = width;
            float aspect = (RenderTargetDebugger.CurrentRT.rt.height / RenderTargetDebugger.CurrentRT.rt.width);
            rect.height = rect.width;

            if (colorChannel == 0) //RGBA
            {
                EditorGUI.DrawTextureTransparent(rect, RenderTargetDebugger.CurrentRT, ScaleMode.ScaleToFit, aspect);
            }
            else if (colorMask == ColorWriteMask.Alpha)
            {
                EditorGUI.DrawTextureAlpha(rect, RenderTargetDebugger.CurrentRT, ScaleMode.ScaleToFit, aspect, 0);
            }
            else
            {
                EditorGUI.DrawPreviewTexture(rect, RenderTargetDebugger.CurrentRT, null, ScaleMode.ScaleToFit, aspect, 0, colorMask, exposure);
            }
            GUILayout.Space(rect.height);
            exposure = EditorGUILayout.Slider("Exposure", exposure, 1f, 16f);
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace StylizedWater3
{
    [CustomEditor(typeof(WaterMeshImporter))]
    public class WaterMeshImporterInspector: ScriptedImporterEditor
    {
        private SerializedProperty waterMesh;
        
        private SerializedProperty shape;
        
        private SerializedProperty scale;
        private SerializedProperty UVTiling;
        
        private SerializedProperty vertexDistance;
        
        private SerializedProperty noise;
        private SerializedProperty boundsPadding;
        
        private WaterMeshImporter importer;
        
		private bool autoApplyChanges;
        private bool previewInSceneView
        {
            get => EditorPrefs.GetBool("SWS2_PREVIEW_WATER_MESH_ENABLED", true);
            set => EditorPrefs.SetBool("SWS2_PREVIEW_WATER_MESH_ENABLED", value);
        }

        public override void OnEnable()
        {
			base.OnEnable();
			
            importer = (WaterMeshImporter)target;
            
            waterMesh = serializedObject.FindProperty("waterMesh");
            
            shape = waterMesh.FindPropertyRelative("shape");
            scale = waterMesh.FindPropertyRelative("scale");
            UVTiling = waterMesh.FindPropertyRelative("UVTiling");
            vertexDistance = waterMesh.FindPropertyRelative("vertexDistance");
            noise = waterMesh.FindPropertyRelative("noise");
            boundsPadding = waterMesh.FindPropertyRelative("boundsPadding");

            SceneView.duringSceneGui += OnSceneGUI;
        }

        public override void OnInspectorGUI()
        {
            UI.DrawHeader();
            
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth);
                previewInSceneView =
                    GUILayout.Toggle(previewInSceneView, new GUIContent("  Preview in scene view", EditorGUIUtility.IconContent(
                        (previewInSceneView ? "animationvisibilitytoggleon" : "animationvisibilitytoggleoff")).image), "Button");
            }
            if (previewInSceneView && WaterObject.Instances.Count > 0)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(EditorGUIUtility.labelWidth);
                    EditorGUILayout.HelpBox($"Drawing on WaterObject instances in the scene ({WaterObject.Instances.Count})", MessageType.None);
                }
            }
            
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(shape);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(scale);
            EditorGUILayout.PropertyField(vertexDistance);
            
            int subdivisions = Mathf.FloorToInt(scale.floatValue / vertexDistance.floatValue);
            int vertexCount = Mathf.FloorToInt(subdivisions * subdivisions);

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth);
                
                EditorGUILayout.HelpBox($"Vertex count: {vertexCount:N1}", MessageType.None);
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(UVTiling);
            EditorGUILayout.PropertyField(noise);
            EditorGUILayout.PropertyField(boundsPadding);
            
            EditorGUILayout.Space();

            autoApplyChanges = EditorGUILayout.Toggle("Auto-apply changes", autoApplyChanges);
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                
                if (autoApplyChanges && HasModified())
                {
                    this.SaveChanges();

                    //importer = (WaterMeshImporter)target;
                }
            }
            
            this.ApplyRevertGUI();
            
            UI.DrawFooter();
        }
        
        private void OnDestroy()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private Material mat;
        private void OnSceneGUI(SceneView obj)
        {
            if (!previewInSceneView)
            {
                GL.wireframe = false;
                return;
            }

            if (!mat)
            {
                mat = new Material(Shader.Find("Unlit/Color"));
                mat.color = new Color(0,0,0, 0.25f);
                mat.mainTexture = Texture2D.whiteTexture;
            }
            mat.SetPass(0);
            
            if (importer.waterMesh.mesh)
            {
                GL.wireframe = true;
                if (WaterObject.Instances.Count > 0)
                {
                    foreach (WaterObject waterObject in WaterObject.Instances)
                    {
                        Graphics.DrawMeshNow(importer.waterMesh.mesh, waterObject.transform.localToWorldMatrix);
                    }
                }
                else
                {
                    if (SceneView.lastActiveSceneView)
                    {
                        //Position in view
                        Vector3 position = SceneView.lastActiveSceneView.camera.transform.position + (SceneView.lastActiveSceneView.camera.transform.forward * importer.waterMesh.scale * 0.5f);

                        Graphics.DrawMeshNow(importer.waterMesh.mesh, position, Quaternion.identity);
                    }
                }
                GL.wireframe = false;
            }
        }
    }
}
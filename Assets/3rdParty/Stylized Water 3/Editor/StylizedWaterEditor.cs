// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
#if URP
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
#endif

namespace StylizedWater3
{
    public class StylizedWaterEditor : Editor
    {
        #if URP
        [MenuItem("GameObject/3D Object/Water/Single Object", false, 0)]
        public static void CreateWaterObject()
        {
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath("e8333e151973f1c4188ff534979c823b"));
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(AssetDatabase.GUIDToAssetPath("d0abf58b1088fc044aaa6d97c5d51d65"));

            WaterObject obj = WaterObject.New(mat, mesh);
            
            //Position in view
            if (SceneView.lastActiveSceneView)
            {
                obj.transform.position = SceneView.lastActiveSceneView.camera.transform.position + (SceneView.lastActiveSceneView.camera.transform.forward * (Mathf.Max(mesh.bounds.size.x, mesh.bounds.size.z)) * 0.5f);
            }
            
            if (Selection.activeGameObject) obj.transform.parent = Selection.activeGameObject.transform;

            Selection.activeObject = obj;
            
            if(Application.isPlaying == false) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("GameObject/3D Object/Water/Grid", false, 1)]
        public static void CreateWaterGrid()
        {
            GameObject obj = new GameObject("Water Grid", typeof(WaterGrid));
            Undo.RegisterCreatedObjectUndo(obj, "Created Water Grid");

            obj.layer = LayerMask.NameToLayer("Water");
            
            WaterGrid grid = obj.GetComponent<WaterGrid>();
            grid.Recreate();

            if (Selection.activeGameObject) obj.transform.parent = Selection.activeGameObject.transform;
            
            Selection.activeObject = obj;

            //Position in view
            if (SceneView.lastActiveSceneView)
            {
                Vector3 position = SceneView.lastActiveSceneView.camera.transform.position + (SceneView.lastActiveSceneView.camera.transform.forward * grid.scale * 0.5f);
                position.y = 0f;
                
                grid.transform.position = position;
            }
            
            if(Application.isPlaying == false) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        
        private const string OCEAN_PREFAB_GUID = "3b290065ab77e714d91b7f9e02c830d7";

        [MenuItem("GameObject/3D Object/Water/Ocean", false, 2)]
        public static void CreateOcean()
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(OCEAN_PREFAB_GUID);

            if (prefabPath == string.Empty)
            {
                Debug.LogError("Failed to find the Ocean prefab with the GUID " + OCEAN_PREFAB_GUID);
                return;
            }

            Object prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Object));
            
            GameObject obj = PrefabUtility.InstantiatePrefab(prefab, Selection.activeGameObject ? Selection.activeGameObject.scene : EditorSceneManager.GetActiveScene()) as GameObject;
            
            Undo.RegisterCreatedObjectUndo(obj, "Ocean");
            
            if (Selection.activeGameObject) obj.transform.parent = Selection.activeGameObject.transform;
            
            Selection.activeObject = obj;
            
            if(Application.isPlaying == false) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        
        [MenuItem("Window/Stylized Water 3/Set up render feature", false, 2000)]
        public static void SetupRenderFeature()
        {
            List<ScriptableRendererData> renderers = PipelineUtilities.SetupRenderFeature<StylizedWaterRenderFeature>("Stylized Water 3");
            
            Debug.Log($"Added the Stylized Water 3 render feature to {renderers.Count} renderers");
        }
        
        [MenuItem("GameObject/3D Object/Water/Planar Reflections Renderer", false, 1000)]
        public static void CreatePlanarReflectionRenderer()
        {
            GameObject obj = new GameObject("Planar Reflections Renderer", typeof(PlanarReflectionRenderer));
            Undo.RegisterCreatedObjectUndo(obj, "Created PlanarReflectionRenderer");
            PlanarReflectionRenderer r = obj.GetComponent<PlanarReflectionRenderer>();
            r.ApplyToAllWaterInstances();

            Selection.activeObject = obj;
            
            if(Application.isPlaying == false) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        #endif
        
        [MenuItem("Assets/Create/Water/Mesh")]
        private static void CreateWaterPlaneAsset()
        {
            ProjectWindowUtil.CreateAssetWithContent("New Watermesh.watermesh", "");
        }
        
        [MenuItem("CONTEXT/Transform/Align To Water")]
        private static void AddAlignToWater(MenuCommand cmd)
        {
            Transform t = (Transform)cmd.context;

            if (!t.gameObject.GetComponent<AlignToWater>())
            {
                t.gameObject.AddComponent<AlignToWater>();

                EditorUtility.SetDirty(t);
            }
        }
        
        public class PopUpMaterialEditor : EditorWindow
        {
            private Object target;
            private MaterialEditor editor;
   
            public static void Create(Object asset)
            {
                var window = CreateWindow<PopUpMaterialEditor>($"{asset.name} | {asset.GetType().Name}");
                window.target = asset;
                window.editor = (MaterialEditor)Editor.CreateEditor(asset, typeof(MaterialEditor));
                //window.editor.OnEnable();
                //window.editor.target = asset as Material;

                window.Show();
            }

            private void OnGUI()
            {
                EditorGUI.BeginDisabledGroup(true);
                target = EditorGUILayout.ObjectField("Asset", target, typeof(Material), false);
                EditorGUI.EndDisabledGroup();

                //editor.OnInspectorGUI();
                editor.PropertiesGUI();
            }
        }
        
        public class PopUpAssetEditor : EditorWindow
        {
            private Object target;
            private Editor editor;
   
            public static void Create(Object asset)
            {
                var window = CreateWindow<PopUpAssetEditor>($"{asset.name} | {asset.GetType().Name}");
                window.target = asset;
                window.editor = Editor.CreateEditor(asset);
                window.editor.target = asset;
                window.ShowModalUtility();
            }

            private void OnGUI()
            {
                EditorGUI.BeginDisabledGroup(true);
                target = EditorGUILayout.ObjectField("Asset", target, target.GetType(), false);
                EditorGUI.EndDisabledGroup();

                editor.OnInspectorGUI();
            }
        }

        public static bool IsRenderFeatureSetup()
        {
            return PipelineUtilities.RenderFeatureAdded<StylizedWaterRenderFeature>();
        }
        
        public static bool UnderwaterRenderingInstalled()
        {
            //Checking for UnderwaterRenderer.cs meta file
            string path = AssetDatabase.GUIDToAssetPath("57d885066d673c04b850d787a2614e48");
            return AssetDatabase.LoadMainAssetAtPath(path);
        }
        
        public static bool DynamicEffectsInstalled()
        {
            //Checking for the RenderFeature.DynamicEffects.cs meta file
            string path = AssetDatabase.GUIDToAssetPath("4f5bfefce00aeb5479fc940fb8e61837");
            return AssetDatabase.LoadMainAssetAtPath(path);
        }

        public static bool CurvedWorldInstalled(out string libraryPath)
        {
            //Checking for "CurvedWorldTransform.cginc"
            libraryPath = AssetDatabase.GUIDToAssetPath("208a98c9ab72b9f4bb8735c6a229e807");
            return libraryPath != string.Empty;
        }

        public static void OpenGraphicsSettings()
        {
            SettingsService.OpenProjectSettings("Project/Graphics");
        }
        
        public static void SelectForwardRenderer()
        {
			#if URP
            if (!UniversalRenderPipeline.asset) return;
            
            Selection.activeObject = PipelineUtilities.GetDefaultRenderer();
			#endif
        }

        public static void EnableDepthTexture()
        {
			#if URP
            if (!UniversalRenderPipeline.asset) return;

            UniversalRenderPipeline.asset.supportsCameraDepthTexture = true;
            EditorUtility.SetDirty(UniversalRenderPipeline.asset);

            PipelineUtilities.IsDepthTextureOptionDisabledAnywhere(out var renderers);
            
            if (renderers.Count > 0)
            {
                string[] rendererNames = new string[renderers.Count];
                for (int i = 0; i < rendererNames.Length; i++)
                {
                    rendererNames[i] = "• " + renderers[i].name;
                }
                
                if (EditorUtility.DisplayDialog(AssetInfo.ASSET_NAME, "The Depth Texture option is still disabled on other pipeline assets (likely for other quality levels):\n\n" +
                                                                      System.String.Join(System.Environment.NewLine, rendererNames) +
                                                                      "\n\nWould you like to enable it on those as well?", "OK", "Cancel"))
                {
                    PipelineUtilities.SetDepthTextureOnAllAssets(true);   
                }
            }
			#endif
        }

        public static void EnableOpaqueTexture()
        {
			#if URP
            if (!UniversalRenderPipeline.asset) return;

            UniversalRenderPipeline.asset.supportsCameraOpaqueTexture = true;
            EditorUtility.SetDirty(UniversalRenderPipeline.asset);

            PipelineUtilities.IsOpaqueTextureOptionDisabledAnywhere(out var renderers);

            if (renderers.Count > 0)
            {
                string[] rendererNames = new string[renderers.Count];
                for (int i = 0; i < rendererNames.Length; i++)
                {
                    rendererNames[i] = "• " + renderers[i].name;
                }
                
                if (EditorUtility.DisplayDialog(AssetInfo.ASSET_NAME, "The Opaque Texture option is still disabled on other pipeline assets (likely for other quality levels):\n\n" +
                                                                      System.String.Join(System.Environment.NewLine, rendererNames) +
                                                                      "\n\nWould you like to enable it on those as well?", "OK", "Cancel"))
                {
                    PipelineUtilities.SetOpaqueTextureOnAllAssets(true);   
                }
            }

			#endif
        }
        
        /// <summary>
        /// Configures the assigned water material to render as double-sided, which is required for underwater rendering
        /// </summary>
        public static void DisableCullingForMaterial(Material material)
        {
            if (!material) return;
            
            material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            
            EditorUtility.SetDirty(material);
        }
    }
}
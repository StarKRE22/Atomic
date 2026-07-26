// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Diagnostics;
using UnityEngine;
#if URP
using UnityEngine.Rendering.Universal;
#endif
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace StylizedWater3.Demo
{
    [ExecuteAlways]
    public class DemoController : MonoBehaviour
    {
        public bool isMainScene;
        
        [Flags]
        public enum Requirements
        {
            None = 0,
            DepthTexture = 1,
            OpaqueTexture = 2,
            Lightcookies = 4,
            Decals = 8,
            RenderFeature = 16,
            HeightPrePass = 32,
            Splines = 64
        }

        public Requirements requirements;
        
        #if UNITY_EDITOR
        [Space]
        public SceneAsset[] sceneAssets = Array.Empty<SceneAsset>();
        #endif
    
        [SerializeField]
        private string[] sceneGUIDS = Array.Empty<string>();
        private string[] scenePaths = Array.Empty<string>();
        
        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            #if UNITY_EDITOR
            sceneGUIDS = new string[sceneAssets.Length];
            scenePaths = new string[sceneAssets.Length];
            for (int i = 0; i < sceneAssets.Length; i++)
            {
                scenePaths[i] = AssetDatabase.GetAssetPath(sceneAssets[i]);
                sceneGUIDS[i] = AssetDatabase.AssetPathToGUID(scenePaths[i]);
            }
            #endif
        }

        [SerializeField]
        private bool realtimeReflectionProbesDisabled;
        
        private void OnEnable()
        {
            #if UNITY_EDITOR
            realtimeReflectionProbesDisabled = QualitySettings.realtimeReflectionProbes;

            if (!realtimeReflectionProbesDisabled)
            {
                Debug.LogWarning("Realtime Reflection Probes are disabled in your Quality Settings, this is by default in new Unity projects. To ensure the water looks correct, it has been enabled temporarily.");
                QualitySettings.realtimeReflectionProbes = true;
            }
            
            string sceneName = this.gameObject.scene.name;
            
            string setupMessage = $"Not all functionality in the  scene \"{sceneName}\" will work as intended, due to incorrect or missing project settings:\n\n";
            bool requiresSetup = false;
            
            #if URP
            if (UniversalRenderPipeline.asset == null) return;

            if (requirements.HasFlag(Requirements.DepthTexture))
            {
                if (UniversalRenderPipeline.asset.supportsCameraDepthTexture == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Depth texture isn't enabled. Water depth will not look correct.\n";
                }
            }
                        
            if (requirements.HasFlag(Requirements.OpaqueTexture))
            {
                if (UniversalRenderPipeline.asset.supportsCameraOpaqueTexture == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Opaque texture isn't enabled. Refraction effect cannot work\n";
                }
            }
            
            if (requirements.HasFlag(Requirements.Lightcookies))
            {
                if (UniversalRenderPipeline.asset.supportsLightCookies == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Light cookies aren't enabled\n";
                }

                LightCookieFormat lightCookieFormat = PipelineUtilities.GetDefaultLightCookieFormat();
                if (lightCookieFormat < LightCookieFormat.ColorHigh)
                {
                    requiresSetup = true;
                    setupMessage += "• Light cookie format isn't set to \"Color High\" or \"Color HDR\"\n";
                }
            }
            
            if (requirements.HasFlag(Requirements.Decals))
            {
                if (PipelineUtilities.IsDecalRenderFeatureSetup() == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Decal render feature isn't set up\n";
                }
            }
            
            if (requirements.HasFlag(Requirements.RenderFeature))
            {
                if (PipelineUtilities.RenderFeatureAdded<StylizedWaterRenderFeature>() == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Stylized Water render feature isn't set up\n";
                }
            }
            
            if (requirements.HasFlag(Requirements.HeightPrePass))
            {
                StylizedWaterRenderFeature renderFeature = (StylizedWaterRenderFeature)PipelineUtilities.GetRenderFeature<StylizedWaterRenderFeature>();
                
                if (renderFeature == null || renderFeature.heightPrePassSettings.enable == false)
                {
                    requiresSetup = true;
                    setupMessage += "• Height pre-pass is disabled on the Stylized Water render feature\n";
                }
            }
            
            if (requirements.HasFlag(Requirements.Splines))
            {
                #if !SPLINES
                requiresSetup = true;
                setupMessage += "• Splines package isn't installed\n";
                #endif
                
            }
            #endif

            setupMessage += "\nThese configurations can be found on your URP renderer. If you're unsure what this means, please consult the \"Getting Started\" documentation section for instructions.";
            
            if(requiresSetup) DisplayPopup(setupMessage);
            #endif
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            OpenScenes();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            #if UNITY_EDITOR
            Scene thisScene = this.gameObject.scene;
            if (isMainScene && scene.GetHashCode() == thisScene.GetHashCode())
            {
                SceneManager.SetActiveScene(thisScene);
            }
            #endif
        }

        void OpenScenes()
        {
            for (int i = 0; i < scenePaths.Length; i++)
            {
                string path = scenePaths[i];

                //Scene not found, possibly an extension not currently installed or a dev-only scene
                if (path != string.Empty)
                {
                    Scene scene = SceneManager.GetSceneByPath(path);

                    if (Application.isPlaying)
                    {
                        if (scene.isLoaded == false)
                        {
                            //Debug.Log($"{scene.name} being loaded");
                            SceneManager.LoadScene(path, LoadSceneMode.Additive);
                        }
                    }
                    else
                    {
                        #if UNITY_EDITOR
                        EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                        #endif
                    }
                }
            }
        }

        private void DisplayPopup(string message)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("Stylized Water 3", message, "OK");
            #endif
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            //Do not meddle with project settings, restore changes
            if (realtimeReflectionProbesDisabled == false && QualitySettings.realtimeReflectionProbes == true) QualitySettings.realtimeReflectionProbes = false;
        }
    }
}
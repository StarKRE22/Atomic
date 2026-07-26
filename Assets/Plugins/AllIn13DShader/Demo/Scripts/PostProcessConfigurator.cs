#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using System.IO;
using UnityEngine;

#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#elif ALLIN13DSHADER_URP
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#endif

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class PostProcessConfigurator : MonoBehaviour
	{
		private Camera cam;

#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
		public PostProcessProfile postProcessProfileBIRP;
#elif ALLIN13DSHADER_URP
		public VolumeProfile postProcessProfileURP;
#endif

		private void Awake()
		{
			cam = GetComponent<Camera>();

			if(cam == null)
			{
				Debug.LogError("Camera not found in this object!", gameObject);
				return;
			}

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
			ConfigurePostprocessBIRP();
#elif ALLIN13DSHADER_URP
				ConfigurePostprocessURP();
#endif
			}
#endif
		}

#if UNITY_EDITOR
	#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
		private void ConfigurePostprocessBIRP()
		{
			PostProcessLayer postProcessLayer;
			if (!cam.gameObject.TryGetComponent<PostProcessLayer>(out postProcessLayer))
			{
				postProcessLayer = cam.gameObject.AddComponent<PostProcessLayer>();
			}

			PostProcessVolume volume;
			if(!cam.gameObject.TryGetComponent<PostProcessVolume>(out volume))
			{
				volume = cam.gameObject.AddComponent<PostProcessVolume>();
			}
			
			postProcessLayer.volumeTrigger = cam.transform;

			if(postProcessProfileBIRP == null)
			{
				string rootFolder = EditorPrefs.GetString("AllIn13DShader_RootPluginFolder", "Assets/AllIn13DShader");
				string profilePath = Path.Combine(rootFolder, "Demo/PostProcessingProfile/3DShaderPP.asset");
				postProcessProfileBIRP = AssetDatabase.LoadAssetAtPath<PostProcessProfile>(profilePath);
			}

			postProcessLayer.volumeLayer = ~0;
			volume.profile = postProcessProfileBIRP;
			volume.isGlobal = true;

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}
#endif

#if ALLIN13DSHADER_URP
		private void ConfigurePostprocessURP()
		{
			Volume volume;
			if (!cam.gameObject.TryGetComponent<Volume>(out volume))
			{
				volume = cam.gameObject.AddComponent<Volume>();
			}

			if(postProcessProfileURP == null)
			{
				string rootFolder = EditorPrefs.GetString("AllIn13DShader_RootPluginFolder", "Assets/AllIn13DShader");
				string profilePath = Path.Combine(rootFolder, "Demo/PostProcessingProfile/3DShaderPP_URP.asset");
				postProcessProfileURP = AssetDatabase.LoadAssetAtPath<VolumeProfile>(profilePath);
			}

			volume.sharedProfile = (VolumeProfile)postProcessProfileURP;
			EditorUtility.SetDirty(volume);

			UniversalAdditionalCameraData cameraData;
			if(!cam.gameObject.TryGetComponent<UniversalAdditionalCameraData>(out cameraData))
			{
				cameraData = cam.gameObject.AddComponent<UniversalAdditionalCameraData>();
			}
			cameraData.renderPostProcessing = true;

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}
#endif
#endif
	}
}
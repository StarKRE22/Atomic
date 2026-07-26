using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Rendering;
using System.IO;


#if ALLIN13DSHADER_URP

using UnityEditor.Rendering.Universal;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.Rendering.Universal;
#else
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
#endif

#endif

namespace AllIn13DShader
{
	public static class URPConfigurator
	{
#if ALLIN13DSHADER_URP

		public static bool IsURPCorrectlyConfigured()
		{
			bool res = true;
			if(GraphicsSettings.currentRenderPipeline == null)
			{
				res = false;
			}

			return res;
		}

		public static void Configure()
		{
			RenderPipelineAsset pipeline = GraphicsSettings.currentRenderPipeline;

			if(pipeline != null)
			{
				string guid;
				long localID;
				AssetDatabase.TryGetGUIDAndLocalFileIdentifier(pipeline, out guid, out localID);

				if(!GlobalConfiguration.instance.URPConfiguredFirstTime || GlobalConfiguration.instance.LastPipelineConfiguredGUID != guid)
				{
					Debug.Log("Configuring plugin to work with URP...");

					ConfigureRenderPipeline(pipeline);

					string demoMaterialsFolder = Path.Combine(GlobalConfiguration.instance.RootPluginPath, "Demo/Materials/StandardExamples");
					if (AssetDatabase.IsValidFolder(demoMaterialsFolder))
					{
						ConvertMaterialsFolder(demoMaterialsFolder);
					}

					GlobalConfiguration.instance.URPConfiguredFirstTime = true;
					GlobalConfiguration.instance.LastPipelineConfiguredGUID = guid;
				}
			}
		}

		public static void ConfigureRenderPipeline(RenderPipelineAsset renderPipelineAsset)
		{
			UniversalRenderPipelineAsset pipeline = renderPipelineAsset as UniversalRenderPipelineAsset;
			ScriptableRenderer scriptableRenderer = pipeline.GetRenderer(0);

			FieldInfo rendererDataListFieldInfo = typeof(UniversalRenderPipelineAsset).GetField("m_RendererDataList", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			ScriptableRendererData[] scriptableRendererDatas = rendererDataListFieldInfo.GetValue(pipeline) as ScriptableRendererData[];

			UniversalRendererData universalRendererData = scriptableRendererDatas[0] as UniversalRendererData;

			Undo.RecordObject(universalRendererData, "");

			universalRendererData.depthPrimingMode = DepthPrimingMode.Disabled;


			string outlinePassName = "OutlinePass";
			string outlineRenderFeatureName = "Render Feature - Outline";

			RenderObjects outlineRenderFeature = null;
			bool outlineRenderFeatureFound = false;

			List<ScriptableRendererFeature> rendererFeatures = universalRendererData.rendererFeatures;
			foreach (ScriptableRendererFeature scriptableRendererFeature in rendererFeatures)
			{
				if (scriptableRendererFeature is RenderObjects)
				{
					RenderObjects renderObjects = (RenderObjects)scriptableRendererFeature;

					string[] passNames = renderObjects.settings.filterSettings.PassNames;
					foreach (string passName in passNames)
					{
						if (passName == outlinePassName)
						{
							outlineRenderFeature = renderObjects;
							outlineRenderFeatureFound = true;
						}
					}
				}

				if (outlineRenderFeatureFound)
				{
					break;
				}
			}

			if (!outlineRenderFeatureFound)
			{
				outlineRenderFeature = RenderObjects.CreateInstance<RenderObjects>();
				outlineRenderFeature.name = outlineRenderFeatureName;

				rendererFeatures.Add(outlineRenderFeature);

				FieldInfo fieldInfoRenderFeaturesMap = typeof(UniversalRendererData).GetField("m_RendererFeatureMap", BindingFlags.Instance | BindingFlags.NonPublic);
				List<long> renderFeaturesMapList = fieldInfoRenderFeaturesMap.GetValue(universalRendererData) as List<long>;

				string guid;
				long localID;
				AssetDatabase.TryGetGUIDAndLocalFileIdentifier(outlineRenderFeature, out guid, out localID);
				renderFeaturesMapList.Add(localID);

				AssetDatabase.AddObjectToAsset(outlineRenderFeature, universalRendererData);
				EditorUtility.SetDirty(outlineRenderFeature);
			}

			outlineRenderFeature.name = outlineRenderFeatureName;
			outlineRenderFeature.settings.filterSettings.RenderQueueType = RenderQueueType.Opaque;
			outlineRenderFeature.settings.filterSettings.LayerMask = ~0;
			outlineRenderFeature.settings.filterSettings.PassNames = new string[] { "OutlinePass" };

			EditorUtility.SetDirty(outlineRenderFeature);
			EditorUtility.SetDirty(universalRendererData);

			FieldInfo fieldInfoRendererFeatures = typeof(UniversalRendererData).GetField("m_RendererFeatures", BindingFlags.Instance | BindingFlags.NonPublic);
			fieldInfoRendererFeatures.SetValue(universalRendererData, rendererFeatures);

			EditorUtility.SetDirty(universalRendererData);
			EditorUtility.SetDirty(pipeline);
			AssetDatabase.SaveAssets();
		}

		public static void ConvertMaterialsFolder(string dirPath)
		{
			StandardUpgrader standardUpgrader = new StandardUpgrader("Standard");

			DirectoryInfo dir = new DirectoryInfo(dirPath);

			List<string> materialsPathsToConvert = new List<string>();
			FileInfo[] files = dir.GetFiles("*.mat");
			for (int i = 0; i < files.Length; i++)
			{
				string materialPath = Path.Combine(dirPath, files[i].Name);
				materialsPathsToConvert.Add(materialPath);
			}

			for (int i = 0; i < materialsPathsToConvert.Count; i++)
			{
				Material mat = AssetDatabase.LoadAssetAtPath<Material>(materialsPathsToConvert[i]);
				standardUpgrader.Upgrade(mat, MaterialUpgrader.UpgradeFlags.None);
			}
		}

		public static void AllAssetProcessed()
		{
			Configure();
		}
#endif
	}
}
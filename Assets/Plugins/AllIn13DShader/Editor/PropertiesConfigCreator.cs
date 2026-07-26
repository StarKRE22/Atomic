using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class PropertiesConfigCreator : AssetPostprocessor
	{
		private const string KEY_PROPERTIES_CONFIG_CREATED_FIRST_TIME = "AllIn13DShader_PropertiesConfigCreatedFirstTime";
		private const string KEY_PIPELINE_PACKAGE_REMOVED = "ALLIN13DSHADER_PIPELINE_PACKAGE_REMOVED_KEY";

		private static PropertiesConfigCollection propertiesCollection;

		private static EffectGroupGlobalConfigCollection effectGroupGlobalConfigCollection;
		private static EffectsExtraData effectsExtraData;


		[MenuItem("Tools/AllIn1/3DShader/Data/Create Properties Configs")]
		public static PropertiesConfigCollection CreateConfig()
		{
			propertiesCollection = CreatePropertiesCollection();
			effectGroupGlobalConfigCollection = GetEffectGroupGlobalConfigCollection();
			effectsExtraData = GetEffectsExtraData();

			for (int i = 0; i < Constants.SHADERS_NAMES.Length; i++)
			{
				CreatePropertiesConfig(propertiesCollection, Constants.SHADERS_NAMES[i]);
			}

			EditorUtility.SetDirty(propertiesCollection);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

			EditorPrefs.SetFloat(Constants.LAST_TIME_SHADER_PROPERTIES_REBUILT_KEY, (float)EditorApplication.timeSinceStartup);

			Debug.LogWarning("Creating data...");

			return propertiesCollection;
		}

		private static PropertiesConfigCollection CreatePropertiesCollection()
		{
			PropertiesConfigCollection res = FindPropertiesCollection();
			string path = AssetDatabase.GetAssetPath(res);

			if (File.Exists(path))
			{
				AssetDatabase.DeleteAsset(path);
				AssetDatabase.Refresh();
			}

			string propertiesCollectionPath = Path.Combine(Constants.SHADERS_PROPERTIES_FOLDER_PATH, "PropertiesConfigCollection.asset");
			res = ScriptableObject.CreateInstance<PropertiesConfigCollection>();
			AssetDatabase.CreateAsset(res, propertiesCollectionPath);
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

			return res;
		}

		private static EffectGroupGlobalConfigCollection GetEffectGroupGlobalConfigCollection()
		{
			string[] guids = AssetDatabase.FindAssets("t:EffectGroupGlobalConfigCollection");
			string path = AssetDatabase.GUIDToAssetPath(guids[0]);

			EffectGroupGlobalConfigCollection res = AssetDatabase.LoadAssetAtPath<EffectGroupGlobalConfigCollection>(path);
			return res;
		}

		private static EffectsExtraData GetEffectsExtraData()
		{
			EffectsExtraData res = null;

			res = EditorUtils.FindAsset<EffectsExtraData>("EffectsExtraData");

			return res;
		}

		private static EffectGroupGlobalConfig[] GetEffectGroups()
		{
			EffectGroupGlobalConfig[] res = new EffectGroupGlobalConfig[0];
			string[] guids = AssetDatabase.FindAssets("t:EffectGroupGlobalConfig");

			for (int i = 0; i < guids.Length; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[i]);
				EffectGroupGlobalConfig effectGroupGlobalConfig = AssetDatabase.LoadAssetAtPath<EffectGroupGlobalConfig>(path);
				ArrayUtility.Add(ref res, effectGroupGlobalConfig);
			}

			return res;
		}

		private static void CreatePropertiesConfig(PropertiesConfigCollection propertiesCollection, string shaderName)
		{
			string shaderNameWithExtension = shaderName + ".shader";
			string assetPath = Path.Combine(Constants.SHADERS_FOLDER_PATH, shaderNameWithExtension);

			Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(assetPath);

			PropertiesConfig propertiesConfig = new PropertiesConfig();
			propertiesConfig.shader = shader;

			propertiesConfig.CreateConfig(effectGroupGlobalConfigCollection, effectsExtraData);

			propertiesCollection.AddConfig(propertiesConfig);
		}

		private static PropertiesConfigCollection FindPropertiesCollection()
		{
			PropertiesConfigCollection res = null;

			string[] guids = AssetDatabase.FindAssets("t:PropertiesConfigCollection");
			if (guids.Length > 0)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
				res = AssetDatabase.LoadAssetAtPath<PropertiesConfigCollection>(assetPath);
			}

			return res;
		}

		static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
		{
			bool removeUniversalForward = false;
			for (int i = 0; i < deletedAssets.Length; i++)
			{
				removeUniversalForward = deletedAssets[i].StartsWith("Packages/com.unity.render-pipelines");
			}

			SessionState.SetBool(KEY_PIPELINE_PACKAGE_REMOVED, removeUniversalForward);


			if (!didDomainReload && SessionState.GetBool(KEY_PIPELINE_PACKAGE_REMOVED, false))
			{
				RenderPipelineChecker.RemovePipelineSymbols();
				SessionState.SetBool(KEY_PIPELINE_PACKAGE_REMOVED, false);

				Debug.LogWarning("Render pipeline package removed. Configuring AllIn13DShader..."); 
			}

			GlobalConfiguration.CheckRootFolder();

			bool configCreatedFirstTime = SessionState.GetBool(KEY_PROPERTIES_CONFIG_CREATED_FIRST_TIME, false);
			bool rebuildShaderProperties = false;

			for (int i = 0; i < importedAssets.Length; i++) 
			{
				rebuildShaderProperties = rebuildShaderProperties || IsAllIn13DShader(importedAssets[i]);
			}

			if (rebuildShaderProperties || !configCreatedFirstTime)
			{
				SessionState.SetBool(KEY_PROPERTIES_CONFIG_CREATED_FIRST_TIME, true);
				ShadersCreatorTool.BuildShaderFiles();
				CreateConfig();
			}

#if ALLIN13DSHADER_URP
			URPConfigurator.AllAssetProcessed();
			AllIn13DShaderWindow.AllAssetProcessed();
#endif
			GlobalConfiguration.instance.Init();
		}

		private static bool IsAllIn13DShader(string importedAsset)
		{
			bool res = false;

			for (int i = 0; i < Constants.SHADERS_NAMES.Length; i++)
			{
				string shaderName = $"{Constants.SHADERS_NAMES[i]}.shader";
				res = res || importedAsset.Contains(shaderName);
			}

			return res;
		}
	}
}
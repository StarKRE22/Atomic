using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AllIn13DShader
{
	public class GlobalConfiguration : ScriptableObject
	{
		private static GlobalConfiguration _instance;
		public static GlobalConfiguration instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = EditorUtils.FindAsset<GlobalConfiguration>("GlobalConfiguration");
				}

				return _instance;
			}
		}


		public enum ProjectType
		{
			[InspectorName("Toon")] TOON = 0,
			[InspectorName("Standard Basic")]STANDARD_BASIC = 1,
			[InspectorName("Standard PBR")]STANDARD_PBR = 2,
			[InspectorName("AllIn13DShader Look")] ALLIN13DSHADERLOOK = 4,
			[InspectorName("Custom")] CUSTOM = 3,
		}
		
		//Project type and default materials
		public ProjectType projectType;

		public Material standardBasicMaterial;
		public Material standardPBRMaterial;
		public Material toonMaterial;
		public Material allIn13dDShaderLookMaterial;

		public Material defaultPreset;

		//Default Relative Paths
		public static string EXPORT_PARENT_FOLDER_RELATIVE = "Export";

		public static string MATERIAL_SAVE_FOLDER_NAME = "Materials";
		public static string RENDER_IMAGE_SAVE_FOLDER_NAME = "Images";
		public static string NORMAL_MAP_SAVE_FOLDER_NAME = "Normal Maps";
		public static string GRADIENT_SAVE_FOLDER_NAME = "Gradients";
		public static string ATLASES_SAVE_FOLDER_NAME = "Atlases";
		public static string NOISES_SAVE_FOLDER_NAME = "Noises";

		public static string MATERIAL_SAVE_RELATIVE_PATH_DEFAULT = "Export/Materials";
		public static string RENDER_IMAGE_SAVE_RELATIVE_PATH_DEFAULT = "Export/Images";
		public static string NORMAL_MAP_SAVE_RELATIVE_PATH_DEFAULT = "Export/Normal Maps";
		public static string GRADIENT_SAVE_RELATIVE_PATH_DEFAULT = "Export/Gradients";
		public static string ATLASES_SAVE_RELATIVE_PATH_DEFAULT = "Export/Atlases";
		public static string NOISES_SAVE_RELATIVE_PATH_DEFAULT = "Export/Noises";


		////Default Fullt Paths
		//public static string MATERIAL_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, MATERIAL_SAVE_RELATIVE_PATH_DEFAULT);
		//public static string RENDER_IMAGE_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, RENDER_IMAGE_SAVE_RELATIVE_PATH_DEFAULT);
		//public static string NORMAL_MAP_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, NORMAL_MAP_SAVE_RELATIVE_PATH_DEFAULT);
		//public static string GRADIENT_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, GRADIENT_SAVE_RELATIVE_PATH_DEFAULT);
		//public static string ATLASES_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, ATLASES_SAVE_RELATIVE_PATH_DEFAULT);
		//public static string NOISES_SAVE_PATH_DEFAULT = Path.Combine(RootPluginPath, NOISES_SAVE_RELATIVE_PATH_DEFAULT);

		//Default Root Plugin Path
		public const string ROOT_PLUGIN_FOLDER_DEFAULT = "Assets/Plugins/AllIn13DShader";

		//Paths
		[SerializeField] private string rootPluginPath;
		[SerializeField] private string materialSavePath;
		[SerializeField] private string renderImageSavePath;
		[SerializeField] private string normalMapSavePath;
		[SerializeField] private string gradientSavePath;
		[SerializeField] private string atlasesSavePath;
		[SerializeField] private string noiseSavePath;

		//Render Image Scale
		[SerializeField] private float renderImageScale;

		//URP Configured first time
		private bool urpConfiguredFirstTime;
		[SerializeField] private string lastPipelineConfiguredGUID;


		public string RootPluginPath
		{
			get
			{
				return rootPluginPath;
			}
			set
			{
				rootPluginPath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string MaterialSavePath
		{
			get
			{
				return materialSavePath;
			}
			set
			{
				materialSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string RenderImageSavePath
		{
			get
			{
				return renderImageSavePath;
			}
			set
			{
				renderImageSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string NormalMapSavePath
		{
			get
			{
				return normalMapSavePath;
			}
			set
			{
				normalMapSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string GradientSavePath
		{
			get
			{
				return gradientSavePath;
			}
			set
			{
				gradientSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string AtlasesSavePath
		{
			get
			{
				return atlasesSavePath;
			}
			set
			{
				atlasesSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string NoiseSavePath
		{
			get
			{
				return noiseSavePath;
			}
			set
			{
				noiseSavePath = value;
				EditorUtility.SetDirty(this);
			}
		}

		public float RenderImageScale
		{
			get
			{
				return renderImageScale;
			}
			set
			{
				renderImageScale = value;
				EditorUtility.SetDirty(this);
			}
		}

		public bool URPConfiguredFirstTime
		{
			get
			{
				return urpConfiguredFirstTime;
			}
			set
			{
				urpConfiguredFirstTime = value;
				EditorUtility.SetDirty(this);
			}
		}

		public string LastPipelineConfiguredGUID
		{
			get
			{
				return lastPipelineConfiguredGUID;
			}
			set
			{
				lastPipelineConfiguredGUID = value;
				EditorUtility.SetDirty(this);
			}
		}

		public void Init()
		{
			rootPluginPath		= InitPath(rootPluginPath, ROOT_PLUGIN_FOLDER_DEFAULT, true);
			materialSavePath	= InitPath(materialSavePath, MATERIAL_SAVE_RELATIVE_PATH_DEFAULT);
			renderImageSavePath = InitPath(renderImageSavePath, RENDER_IMAGE_SAVE_RELATIVE_PATH_DEFAULT);
			normalMapSavePath	= InitPath(normalMapSavePath, NORMAL_MAP_SAVE_RELATIVE_PATH_DEFAULT);
			gradientSavePath	= InitPath(gradientSavePath, GRADIENT_SAVE_RELATIVE_PATH_DEFAULT);
			atlasesSavePath		= InitPath(atlasesSavePath, ATLASES_SAVE_RELATIVE_PATH_DEFAULT);
			noiseSavePath		= InitPath(noiseSavePath, NOISES_SAVE_RELATIVE_PATH_DEFAULT);

			bool foldersCreated = false;
			CreateDefaultExportFoldersIfNotExist(ref foldersCreated);

			if (foldersCreated)
			{
				AssetDatabase.Refresh();
			}
		}

		public string InitPath(string path, string defaultValue, bool isRoot = false)
		{
			string res = path;
			if (!AssetDatabase.IsValidFolder(res))
			{
				string defaultPath = defaultValue;
				if (!isRoot)
				{
					defaultPath = Path.Combine(rootPluginPath, defaultValue);
				}
				res = defaultPath;

				EditorUtility.SetDirty(this);
			}

			return res;
		}

		public void CreateDefaultExportFoldersIfNotExist(ref bool foldersCreated)
		{
			string parentExportAbsoluteFolderPath = Path.Combine(rootPluginPath, EXPORT_PARENT_FOLDER_RELATIVE);
			CreateFolderIfNotExist(parentExportAbsoluteFolderPath, rootPluginPath, EXPORT_PARENT_FOLDER_RELATIVE, ref foldersCreated);

			CreateFolderIfNotExist(materialSavePath, parentExportAbsoluteFolderPath, MATERIAL_SAVE_FOLDER_NAME, ref foldersCreated);
			CreateFolderIfNotExist(renderImageSavePath, parentExportAbsoluteFolderPath, RENDER_IMAGE_SAVE_FOLDER_NAME, ref foldersCreated);
			CreateFolderIfNotExist(normalMapSavePath, parentExportAbsoluteFolderPath, NORMAL_MAP_SAVE_FOLDER_NAME, ref foldersCreated);
			CreateFolderIfNotExist(gradientSavePath, parentExportAbsoluteFolderPath, GRADIENT_SAVE_FOLDER_NAME, ref foldersCreated);
			CreateFolderIfNotExist(atlasesSavePath, parentExportAbsoluteFolderPath, ATLASES_SAVE_FOLDER_NAME, ref foldersCreated);
			CreateFolderIfNotExist(noiseSavePath, parentExportAbsoluteFolderPath, NOISES_SAVE_FOLDER_NAME, ref foldersCreated);
		}

		private void CreateFolderIfNotExist(string absoluteFolderPath, string parentFolder, string relativePath, ref bool foldersCreated)
		{
			if (!AssetDatabase.IsValidFolder(absoluteFolderPath))
			{
				AssetDatabase.CreateFolder(parentFolder, relativePath);
				foldersCreated = foldersCreated || true;
			}
		}

		public void RefreshDefaultMaterial()
		{
			switch (projectType)
			{
				case ProjectType.STANDARD_BASIC:
					this.defaultPreset = standardBasicMaterial;
					break;
				case ProjectType.STANDARD_PBR:
					this.defaultPreset = standardPBRMaterial;
					break;
				case ProjectType.TOON:
					this.defaultPreset = toonMaterial;
					break;
				case ProjectType.ALLIN13DSHADERLOOK:
					this.defaultPreset = allIn13dDShaderLookMaterial;
					break;
			}
		}

		public void RootFolderChanged(string oldRootFolder)
		{
			MaterialSavePath	= UpdateRootFolders(oldRootFolder, MaterialSavePath, MATERIAL_SAVE_RELATIVE_PATH_DEFAULT);
			RenderImageSavePath = UpdateRootFolders(oldRootFolder, RenderImageSavePath, RENDER_IMAGE_SAVE_RELATIVE_PATH_DEFAULT);
			NormalMapSavePath	= UpdateRootFolders(oldRootFolder, NormalMapSavePath, NORMAL_MAP_SAVE_RELATIVE_PATH_DEFAULT);
			GradientSavePath	= UpdateRootFolders(oldRootFolder, GradientSavePath, GRADIENT_SAVE_RELATIVE_PATH_DEFAULT);
			AtlasesSavePath		= UpdateRootFolders(oldRootFolder, AtlasesSavePath, ATLASES_SAVE_RELATIVE_PATH_DEFAULT);
			NoiseSavePath		= UpdateRootFolders(oldRootFolder, NoiseSavePath, NOISES_SAVE_RELATIVE_PATH_DEFAULT);
		}

		private string UpdateRootFolders(string oldRootFolder, string pathToCheck, string relativePath)
		{
			string res = pathToCheck;

			string pathToCheckFull = Path.GetFullPath(pathToCheck);
			string pathWithOldRootFull = Path.GetFullPath(Path.Combine(oldRootFolder, relativePath)); 

			if (pathToCheckFull == pathWithOldRootFull)
			{
				res = Path.Combine(RootPluginPath, relativePath);
			}

			return res;
		}

		public static void CheckRootFolder()
		{
			string newRootFolder = GetRootPluginPath();

			if (newRootFolder != instance.RootPluginPath)
			{
				string oldRootFolder = instance.RootPluginPath;
				instance.RootPluginPath = newRootFolder;
				instance.RootFolderChanged(oldRootFolder);
			}
		}

		private static string GetRootPluginPath()
		{
			Object mainAssemblyAsset = EditorUtils.FindAsset<AssemblyDefinitionAsset>("AllIn13DShaderAssemebly");
			string assetPath = AssetDatabase.GetAssetPath(mainAssemblyAsset);

			string res = Path.GetDirectoryName(assetPath);
			return res;
		}

		public void Save()
		{
			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssetIfDirty(this);
		}
	}
}
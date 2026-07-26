using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public static class RightClickMaterialCreator
	{
		private const string MENU_PATH = "Assets/Create/AllIn13DShader/Materials";

		public static void CreateMaterial(Material matSource)
		{
			string saveFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

			if (!AssetDatabase.IsValidFolder(saveFolderPath))
			{
				saveFolderPath = GlobalConfiguration.instance.MaterialSavePath;
			}

			Material mat = new Material(matSource);

			string materialPath = Path.Combine(saveFolderPath, AllIn13DShaderConfig.MATERIAL_NAME_DEFAULT);
			materialPath = AssetDatabase.GenerateUniqueAssetPath(materialPath);
			AssetDatabase.CreateAsset(mat, materialPath);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			Selection.activeObject = mat;
		}

		private static GlobalConfiguration GetGlobalConfiguration()
		{
			GlobalConfiguration res = EditorUtils.FindAsset<GlobalConfiguration>("GlobalConfiguration");
			return res;
		}

		[MenuItem(MENU_PATH + "/Default Material", false, 1)]
		public static void CreateMaterialDefault()
		{
			GlobalConfiguration globalConfiguration = GetGlobalConfiguration();
			CreateMaterial(globalConfiguration.defaultPreset);
		}

		[MenuItem(MENU_PATH + "/Toon Material", false, 1)]
		public static void CreateMaterialToon()
		{
			GlobalConfiguration globalConfiguration = GetGlobalConfiguration();
			CreateMaterial(globalConfiguration.toonMaterial);
		}

		[MenuItem(MENU_PATH + "/PBR Material", false, 1)]
		public static void CreateMaterialPBR()
		{
			GlobalConfiguration globalConfiguration = GetGlobalConfiguration();
			CreateMaterial(globalConfiguration.standardPBRMaterial);
		}

		[MenuItem(MENU_PATH + "/Basic Lighting Material", false, 1)]
		public static void CreateMaterialBasic()
		{
			GlobalConfiguration globalConfiguration = GetGlobalConfiguration();
			CreateMaterial(globalConfiguration.standardBasicMaterial);
		}

		[MenuItem(MENU_PATH + "/AllIn13D Look", false, 1)]
		public static void CreateMaterialAllIn3DLook()
		{
			GlobalConfiguration globalConfiguration = GetGlobalConfiguration();
			CreateMaterial(globalConfiguration.allIn13dDShaderLookMaterial);
		}
	}
}
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public static class MaterialConverterTool
	{
		private const int OVERRIDE = 0;
		private const int CANCEL = 1;
		private const int KEEP_ORIGINALS = 2;

		[MenuItem(itemName: "Assets/AllIn1/Convert ALL materials to AllIn13DShader", isValidateFunction: true)]
		public static bool ValidateBatchConvert()
		{
			bool res = true;

			Object[] selectedAssets = Selection.GetFiltered<Object>(SelectionMode.Assets);
			foreach (Object asset in selectedAssets)
			{
				string path = AssetDatabase.GetAssetPath(asset);
				
				bool isValidFolder = AssetDatabase.IsValidFolder(path);

				Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
				bool isMaterial = mat != null;

				res = res && (isValidFolder || isMaterial);
			}

			return res;
		}

		[MenuItem(itemName: "Assets/AllIn1/Convert ALL materials to AllIn13DShader")]
		public static void BatchConvert()
		{
			Object[] selectedAssets = Selection.GetFiltered<Object>(SelectionMode.Assets);
			HashSet<string> pathsToConvert = CollectPathsToConvert(selectedAssets);

			string title = "Converting materials to AllIn13D";
			string message = $"You are about to convert {pathsToConvert.Count} materials to AllIn13D";
			string okButton = "Convert and Override";
			string altButton = "Convert and keep originals";
			string cancelButton = "Cancel";


			int dialog = EditorUtility.DisplayDialogComplex(title, message, okButton, cancelButton, altButton);
			
			if(dialog == CANCEL) { return; }

			ConvertMaterials(pathsToConvert, dialog);
		}
		
		[MenuItem(itemName: "Assets/AllIn1/Convert Standard materials to AllIn13DShader")]
		public static void BatchConvertStandardMats()
		{
			Object[] selectedAssets = Selection.GetFiltered<Object>(SelectionMode.Assets);
			HashSet<string> pathsToConvert = CollectPathsToConvert(selectedAssets);
			
			//Remove pathsToConvert of materials that have a shader name that contains Standard
			HashSet<string> standardMaterialPaths = new HashSet<string>(pathsToConvert.Count);
			foreach(string path in pathsToConvert)
			{
				Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
				if(mat != null && mat.shader != null && mat.shader.name.Contains("Standard"))
					standardMaterialPaths.Add(path);
			}

			string title = "Converting Standard materials to AllIn13D";
			string message = $"You are about to convert {standardMaterialPaths.Count} Standard materials to AllIn13D";
			string okButton = "Convert and Override";
			string altButton = "Convert and keep originals";
			string cancelButton = "Cancel";

			int dialog = EditorUtility.DisplayDialogComplex(title, message, okButton, cancelButton, altButton);
    
			if(dialog == CANCEL) return;

			ConvertMaterials(standardMaterialPaths, dialog);
		}

		private static HashSet<string> CollectPathsToConvert(Object[] selectedAssets)
		{
			HashSet<string> res = new HashSet<string>();

			foreach (Object asset in selectedAssets)
			{
				string path = AssetDatabase.GetAssetPath(asset);

				if (asset is Material)
				{
					res.Add(path);
				}
				else
				{
					string[] materialsInFolderGUIDs = AssetDatabase.FindAssets("t: Material", new string[] { path });
					foreach (string guid in materialsInFolderGUIDs)
					{
						res.Add(AssetDatabase.GUIDToAssetPath(guid));
					}
				}
			}

			return res;
		}

		private static void ConvertMaterials(HashSet<string> materialsPaths, int dialog)
		{
			Undo.IncrementCurrentGroup();

			Material matTemplate = GlobalConfiguration.instance.defaultPreset;
			foreach (string path in materialsPaths)
			{
				ConvertMaterial(path, matTemplate, dialog);
			}

			Undo.SetCurrentGroupName("Materials conversion to AllIn13DShader");

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		private static void ConvertMaterial(string path, Material matTemplate, int dialog)
		{
			Material matFrom = AssetDatabase.LoadAssetAtPath<Material>(path);
			Material target = new Material(matTemplate);

			ApplyConversion(matFrom, target);

			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			string folder = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);

			string fileNameNewAsset = fileNameWithoutExtension + "_AllIn13D" + ".mat";
			string pathNewAsset = Path.Combine(folder, fileNameNewAsset);

			if (dialog == KEEP_ORIGINALS)
			{
				AssetDatabase.CreateAsset(target, pathNewAsset);
				Object createdObject = AssetDatabase.LoadAssetAtPath<Object>(pathNewAsset);
			}
			else if (dialog == OVERRIDE)
			{
				Undo.RecordObject(matFrom, "Material converted");

				matFrom.shader = target.shader;
				matFrom.CopyMatchingPropertiesFromMaterial(target);
				EditorUtility.SetDirty(matFrom);
			}
		}

		public static Material Convert(Shader shader, Material from)
		{
			Material res = new Material(shader);
			Texture normalMap = from.GetTexture("_BumpMap");

			if (normalMap != null)
			{
				res.SetTexture("_NormalMap", normalMap);
			}

			return res;
		}

		public static ConverterGeneral GetConverterByShader(Shader shader)
		{
			ConverterGeneral res = new ConverterGeneral();

			if(shader.name == "Standard")
			{
				res = new ConverterStandardBIRP();
			}
			else if(shader.name == "Universal Render Pipeline/Lit")
			{
				res = new ConverterURPLit();
			}

			return res;
		}

		public static void ApplyConversion(Material from, Material target)
		{
			if(from != null && target != null)
			{
				ConverterGeneral converter = GetConverterByShader(from.shader);
				converter.ApplyConversion(from, target);
			}
		}

		public static void ApplyConversionProperty(ConversionProperty conversionProperty, Material from, Material target, ref bool propertyActive)
		{
			string propertyNameFrom = string.Empty;
			string propertyNameTarget = conversionProperty.propertyName;

			if (from.HasProperty(conversionProperty.propertyName))
			{
				propertyNameFrom = conversionProperty.propertyName;
			}
			else
			{
				for (int i = 0; i < conversionProperty.alternativeNames.Length; i++)
				{
					if (from.HasProperty(conversionProperty.alternativeNames[i]))
					{
						propertyNameFrom = conversionProperty.alternativeNames[i];
						break;
					}
				}
			}

			if (!string.IsNullOrEmpty(propertyNameFrom))
			{
				switch (conversionProperty.propertyType)
				{
					case ConversionPropertyType.TEXTURE:
						Texture texValue = from.GetTexture(propertyNameFrom);
						target.SetTexture(propertyNameTarget, texValue);
						
						propertyActive = texValue != null;

						break;
					case ConversionPropertyType.FLOAT:
						float floatValue = from.GetFloat(propertyNameFrom);
						target.SetFloat(propertyNameTarget, floatValue);

						propertyActive = true;

						break;
					case ConversionPropertyType.COLOR:
						Color colorValue = from.GetColor(propertyNameFrom);
						target.SetColor(propertyNameTarget, colorValue);

						propertyActive = true;

						break;
					case ConversionPropertyType.VECTOR:
						Vector4 vectorValue = from.GetVector(propertyNameFrom);
						target.SetVector(propertyNameTarget, vectorValue);

						propertyActive = true;

						break;
				}
			}
		}
	}
}
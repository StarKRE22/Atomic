using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AllIn13DShader
{
	public static class EditorUtils
	{
		public static T FindAsset<T>(string assetName) where T : Object
		{
			T res = null;

			string[] guids = AssetDatabase.FindAssets($"{assetName} t:{typeof(T).Name}");

			if (guids.Length > 0)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[0]);
				res = AssetDatabase.LoadAssetAtPath<T>(path);
			}	
			
			return res;
		}

		public static T FindAssetByName<T>(string assetName) where T : Object
		{
			T res = null;

			string filter = $"t:{typeof(T)} {assetName}";
			string[] guids = AssetDatabase.FindAssets(filter);

			if (guids.Length > 0)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[0]);
				res = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
			}

			return res;
		}

		public static void PingPath(string assetPath)
		{
			Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture));
			if(asset != null)
			{
				EditorGUIUtility.PingObject(asset);
			}
		}

		public static void ShowNotification(string message)
		{
			SceneView.lastActiveSceneView.ShowNotification(new GUIContent(message));
		}

		public static void DrawThinLine()
		{
			DrawLine(Color.grey, 1, 3);
		}

		public static void DrawLine(Color color, int thickness = 2, int padding = 10)
		{
			Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
			r.height = thickness;
			r.y += (padding / 2);
			r.x -= 2;
			r.width += 6;
			EditorGUI.DrawRect(r, color);
		}

		public static void SetTextureReadWrite(string assetPath, bool enable)
		{
			TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
			if (tImporter != null)
			{
				tImporter.isReadable = enable;
				tImporter.SaveAndReimport();
			}
		}

		public static string DrawSelectorFolder(string initialPath, /*string defaultPath,*/ string label)
		{
			DefaultAsset folderAsset = AssetDatabase.LoadAssetAtPath<DefaultAsset>(initialPath);
			folderAsset = (DefaultAsset)EditorGUILayout.ObjectField(label, folderAsset, typeof(DefaultAsset), false, GUILayout.MaxWidth(500));

			string pathCandidate = AssetDatabase.GetAssetPath(folderAsset);

			string res = initialPath;
			if (Directory.Exists(pathCandidate))
			{
				res = pathCandidate;
			}
			/*
			else
			{
				res = defaultPath;
			}
			*/

			return res;
		}

		public static Texture SaveTextureAsPNG(string folderPath, string fileName, string prefixNotification, Texture2D texture, 
			FilterMode filterMode, TextureImporterType importerType, TextureWrapMode wrapMode, 
			bool askForLocation = true, bool generateUniqueAssetPath = true)
		{
			Texture res = null;

			string fileNameWithExtension = fileName + ".png";
			string path = Path.Combine(folderPath, fileNameWithExtension);

			if (generateUniqueAssetPath)
			{
				path = AssetDatabase.GenerateUniqueAssetPath(path);
			}
			
			fileName = Path.GetFileNameWithoutExtension(path);
			
			if (askForLocation)
			{
				path = EditorUtility.SaveFilePanel("Save texture as PNG", folderPath, fileName, "png");
			}

			if (!string.IsNullOrEmpty(path))
			{
				byte[] pngData = texture.EncodeToPNG();
				if (pngData != null) File.WriteAllBytes(path, pngData);
				AssetDatabase.Refresh();

				if (path.IndexOf("Assets/") >= 0)
				{
					string subPath = path.Substring(path.IndexOf("Assets/"));
					TextureImporter importer = AssetImporter.GetAtPath(subPath) as TextureImporter;
					if (importer != null)
					{
						ShowNotification($"{prefixNotification} saved inside the project: " + subPath);
						
						importer.filterMode = filterMode;
						importer.textureType = importerType;
						importer.wrapMode = wrapMode;

						importer.SaveAndReimport();
						res = AssetDatabase.LoadAssetAtPath<Texture>(subPath);
						EditorGUIUtility.PingObject(res);
					}
				}
				else 
				{
					ShowNotification($"{prefixNotification} saved outside the project: " + path);
				}
			}

			return res;
		}

		public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			targetWidth = Mathf.ClosestPowerOfTwo(targetWidth);
			targetHeight = Mathf.ClosestPowerOfTwo(targetHeight);

			Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
			Color[] scaledPixels = result.GetPixels(0);
			float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
			float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
			for (int px = 0; px < scaledPixels.Length; px++) scaledPixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * (float)Mathf.Floor(px / targetWidth));

			result.SetPixels(scaledPixels, 0);
			result.Apply();
			return result;
		}

		public static void TextureEditorFloatParameter(string parameterName, ref float parameter, float rangeMin = -100f, float rangeMax = 100f, float resetValue = 0f)
		{
			EditorGUILayout.BeginHorizontal();
			{
				parameter = EditorGUILayout.Slider(parameterName, parameter, rangeMin, rangeMax, GUILayout.MaxWidth(400));
				GUIContent resetButtonLabel = new GUIContent
				{
					text = "R",
					tooltip = "Resets to default value"
				};
				if (GUILayout.Button(resetButtonLabel, GUILayout.Width(20))) parameter = resetValue;
			}
			EditorGUILayout.EndHorizontal();
		}

		public static void TextureEditorColorParameter(string parameterName, ref Color parameter, Color resetValue)
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUIContent colorLabel = new GUIContent
				{
					text = parameterName,
					tooltip = parameterName
				};
				parameter = EditorGUILayout.ColorField(colorLabel, parameter, true, true, true, GUILayout.MaxWidth(400));
				GUIContent resetButtonLabel = new GUIContent
				{
					text = "R",
					tooltip = "Resets to default value"
				};
				if (GUILayout.Button(resetButtonLabel, GUILayout.Width(20))) parameter = resetValue;
			}
			EditorGUILayout.EndHorizontal();
		}

		public static void TextureEditorIntParameter(string parameterName, ref int parameter, int rangeMin = -100, int rangeMax = 100, int resetValue = 0)
		{
			EditorGUILayout.BeginHorizontal();
			{
				parameter = EditorGUILayout.IntSlider(parameterName, parameter, rangeMin, rangeMax, GUILayout.MaxWidth(400));
				GUIContent resetButtonLabel = new GUIContent
				{
					text = "R",
					tooltip = "Resets to default value"
				};
				if (GUILayout.Button(resetButtonLabel, GUILayout.Width(20))) parameter = resetValue;
			}
			EditorGUILayout.EndHorizontal();
		}

		public static Shader FindShader(string shaderName)
		{
			string[] guids = AssetDatabase.FindAssets($"{shaderName} t:shader");
			foreach (string guid in guids)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
				if (shader != null)
				{
					string fullShaderName = shader.name;
					string actualShaderName = fullShaderName.Substring(fullShaderName.LastIndexOf('/') + 1);
					if (actualShaderName == shaderName) return shader;
				}
			}
			return null;
		}

		public static void SetDirtyCurrentScene()
		{
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		public static bool IsProjectAsset(Object objectToCheck)
		{
			bool res = false;

			if(objectToCheck != null)
			{
				res = AssetDatabase.Contains(objectToCheck);
			}
			return res;
		}

		public static int GetNumLines(string input)
		{
			int res = input.Split("\n").Length;
			return res;
		}

		public static bool IsAllIn13DShader(string shaderName)
		{
			bool res = false;

			for (int i = 0; i < Constants.SHADERS_NAMES.Length; i++)
			{
				res = res || shaderName.Contains(Constants.SHADERS_NAMES[i]);
			}

			return res;
		}

		public static bool IsAllIn13DShader(Shader shader)
		{
			return IsAllIn13DShader(shader.name);
		}

		public static Color GetRandomColor()
		{
			Color res = new Color(Random.value, Random.value, Random.value, 1.0f);
			return res;
		}

		public static void DrawButtonLink(string url)
		{
			if (EditorGUILayout.LinkButton(url))
			{
				Application.OpenURL(url);
			}
		}
	}
}

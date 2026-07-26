using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class RenderMaterialToImageTool
	{
		public static void RenderAndSaveTexture(Material targetMaterial, Texture targetTexture, float scaleSlider, string folderPath, string fileName)
		{
			RenderTexture renderTarget = new RenderTexture((int)(targetTexture.width * scaleSlider),
				(int)(targetTexture.height * scaleSlider), 0, RenderTextureFormat.ARGB32);
			Graphics.Blit(targetTexture, renderTarget, targetMaterial);
			Texture2D resultTex = new Texture2D(renderTarget.width, renderTarget.height, TextureFormat.ARGB32, false);
			resultTex.ReadPixels(new Rect(0, 0, renderTarget.width, renderTarget.height), 0, 0);
			resultTex.Apply();
			
			if (!Directory.Exists(folderPath))
			{
				EditorUtility.DisplayDialog("The desired Material to Image Save Path doesn't exist",
					"Go to Window -> AllIn1VfxWindow and set a valid folder", "Ok");
				return;
			}

			string fullPath = Path.Combine(folderPath, fileName + ".png");
			fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

			string correctedFileName = fullPath.Replace(folderPath, string.Empty);
			
			fullPath = EditorUtility.SaveFilePanel("Save Render Image", folderPath, fileName, "png");

			byte[] bytes = resultTex.EncodeToPNG();
			File.WriteAllBytes(fullPath, bytes);
			AssetDatabase.ImportAsset(fullPath);
			AssetDatabase.Refresh();
			GameObject.DestroyImmediate(resultTex);

			EditorUtils.PingPath(fullPath);

			EditorUtils.ShowNotification("Render Image saved to: " + fullPath + " with scale: " + scaleSlider +
			" (it can be changed in Tools -> AllIn1 -> 3DShaderWindow)");
		}
	}
}
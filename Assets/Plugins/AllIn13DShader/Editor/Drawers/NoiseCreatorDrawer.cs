using System.IO;
using UnityEditor;
using UnityEngine;
using static AllIn13DShader.NoiseCreatorTool;

namespace AllIn13DShader
{
	public class NoiseCreatorDrawer
	{
		private NoiseCreatorTool noiseCreatorTool;
		private CommonStyles commonStyles;

		private NoiseCreatorValues Values
		{
			get
			{
				return noiseCreatorTool.values;
			}
		}

		public NoiseCreatorDrawer(NoiseCreatorTool noiseCreatorTool, CommonStyles commonStyles)
		{
			this.noiseCreatorTool = noiseCreatorTool;
			this.commonStyles = commonStyles;
		}

		public void Draw()
		{
			GUILayout.Label("Tileable Noise Creator", commonStyles.bigLabel);
			GUILayout.Space(20);

			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.BeginVertical(GUILayout.MaxWidth(550));
				{
					if (Values.noisePreview == null)
					{
						GUILayout.Label("*Change a property to start editing a Noise texture", EditorStyles.boldLabel);
					}

					EditorGUI.BeginChangeCheck();
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label("Noise Type:", GUILayout.MaxWidth(145));
						Values.noiseType = (NoiseTypes)EditorGUILayout.EnumPopup(Values.noiseType, GUILayout.MaxWidth(200));
					}
					EditorGUILayout.EndHorizontal();
					if (EditorGUI.EndChangeCheck())
					{
						noiseCreatorTool.NoiseSetMaterial();
						noiseCreatorTool.CheckCreationNoiseTextures();
						noiseCreatorTool.UpdateNoiseMatAndRender();
					}

					EditorGUI.BeginChangeCheck();
					if (Values.isFractalNoise)
					{
						EditorUtils.TextureEditorFloatParameter("Scale X", ref Values.noiseScaleX, 0.1f, 50f, 4f);
						if (!Values.noiseSquareScale) EditorUtils.TextureEditorFloatParameter("Scale Y", ref Values.noiseScaleY, 0.1f, 50f, 4f);
					}
					else
					{
						EditorUtils.TextureEditorFloatParameter("Scale X", ref Values.noiseScaleX, 0.1f, 50f, 10f);
						if (!Values.noiseSquareScale) EditorUtils.TextureEditorFloatParameter("Scale Y", ref Values.noiseScaleY, 0.1f, 50f, 10f);
					}
					Values.noiseSquareScale = EditorGUILayout.Toggle("Square Scale?", Values.noiseSquareScale, GUILayout.MaxWidth(200));
					if (Values.noiseSquareScale) Values.noiseScaleY = Values.noiseScaleX;
					if (Values.noiseType == NoiseTypes.Fractal) EditorUtils.TextureEditorFloatParameter("Fractal Amount", ref Values.noiseFractalAmount, 1f, 10f, 8f);
					else if (Values.noiseType == NoiseTypes.Perlin) EditorUtils.TextureEditorFloatParameter("Fractal Amount", ref Values.noiseFractalAmount, 1f, 10f, 1f);
					else if (Values.noiseType == NoiseTypes.Billow) EditorUtils.TextureEditorFloatParameter("Fractal Amount", ref Values.noiseFractalAmount, 1f, 10f, 4f);
					else EditorUtils.TextureEditorFloatParameter("Jitter", ref Values.noiseJitter, 0.0f, 2f, 1f);
					EditorUtils.TextureEditorFloatParameter("Contrast", ref Values.noiseContrast, 0.1f, 10f, 1f);
					EditorUtils.TextureEditorFloatParameter("Brightness", ref Values.noiseBrightness, -1f, 1f, 0f);
					EditorUtils.TextureEditorIntParameter("Random Seed", ref Values.noiseSeed, 0, 100, 0);
					Values.noiseInverted = EditorGUILayout.Toggle("Inverted?", Values.noiseInverted);

					if (EditorGUI.EndChangeCheck())
					{
						if (Values.noiseMaterial == null)
						{
							noiseCreatorTool.NoiseSetMaterial();
						}
						noiseCreatorTool.CheckCreationNoiseTextures();

						noiseCreatorTool.UpdateNoiseMatAndRender();
					}

					GUILayout.Space(20);
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label("Noise Size:", GUILayout.MaxWidth(145));
						Values.noiseSize = (TextureSizes)EditorGUILayout.EnumPopup(Values.noiseSize, GUILayout.MaxWidth(200));
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label("New Noise Filtering: ", GUILayout.MaxWidth(145));
						Values.noiseFiltering = (FilterMode)EditorGUILayout.EnumPopup(Values.noiseFiltering, GUILayout.MaxWidth(200));
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.EndVertical();

				if (Values.noisePreview != null) GUILayout.Label(Values.noisePreview, GUILayout.MaxWidth(450), GUILayout.MaxHeight(450));
			}
			EditorGUILayout.EndHorizontal();

			
			GUILayout.Space(20);
			GUILayout.Label("Select the folder where new Noise Textures will be saved", EditorStyles.boldLabel);
			GlobalConfiguration.instance.NoiseSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.NoiseSavePath, /*AllIn13DShaderConfig.NOISES_SAVE_PATH_DEFAULT,*/ "Noises");

			if (Directory.Exists(GlobalConfiguration.instance.NoiseSavePath) && Values.noisePreview != null)
			{
				if (GUILayout.Button("Save Noise Texture", GUILayout.MaxWidth(CommonStyles.BUTTON_WIDTH)))
				{
					noiseCreatorTool.CreateNoiseTex();
					EditorUtils.SaveTextureAsPNG(GlobalConfiguration.instance.NoiseSavePath, "Noise", "Noises", 
						Values.finalNoiseTex, Values.noiseFiltering, TextureImporterType.Default, TextureWrapMode.Clamp);
				}
			}
		}
	}
}
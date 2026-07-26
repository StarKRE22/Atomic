using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class GradientCreatorDrawer
	{
		private GradientCreatorTool gradientCreatorTool;
		private CommonStyles commonStyles;

		private Texture2D GradientTex
		{
			get
			{
				return gradientCreatorTool.gradientTex;
			}
			set
			{
				gradientCreatorTool.gradientTex = value;
			}
		}

		private Gradient Grad
		{
			get
			{
				return gradientCreatorTool.gradient;
			}
			set
			{
				gradientCreatorTool.gradient = value;
			}
		}

		private TextureSizes GradientSizes
		{
			get	
			{
				return gradientCreatorTool.gradientSizes;
			}
			set
			{
				gradientCreatorTool.gradientSizes = value;
			}
		}

		private FilterMode GradientFiltering
		{
			get
			{
				return gradientCreatorTool.gradientFiltering;
			}
			set
			{
				gradientCreatorTool.gradientFiltering = value;
			}
		}

		public GradientCreatorDrawer(GradientCreatorTool gradientCreatorTool, CommonStyles commonStyles)
		{
			this.gradientCreatorTool = gradientCreatorTool;
			this.commonStyles = commonStyles;
		}

		public void Draw()
		{
			GUILayout.Label("Color Gradient Creator", commonStyles.bigLabel);
			GUILayout.Space(20);
			GUILayout.Label("This feature can be used to create textures for the Color Ramp Effect", EditorStyles.boldLabel);

			EditorGUI.BeginChangeCheck();
			Grad = EditorGUILayout.GradientField("Color Gradient: ", Grad, GUILayout.Height(25), GUILayout.MaxWidth(CommonStyles.BUTTON_WIDTH));
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Texture Size:", GUILayout.MaxWidth(145));
				GradientSizes = (TextureSizes)EditorGUILayout.EnumPopup(GradientSizes, GUILayout.MaxWidth(200));
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("New Textures Filtering: ", GUILayout.MaxWidth(145));
				GradientFiltering = (FilterMode)EditorGUILayout.EnumPopup(GradientFiltering, GUILayout.MaxWidth(200));
			}
			EditorGUILayout.EndHorizontal();
			
			bool gradientChanged = EditorGUI.EndChangeCheck();
			if (gradientChanged)
			{
				gradientCreatorTool.CreateGradientTexture();
			}

			GUILayout.Space(20);
			GUILayout.Label("Select the folder where new Color Gradient Textures will be saved", EditorStyles.boldLabel);

			GlobalConfiguration.instance.GradientSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.GradientSavePath, /*AllIn13DShaderConfig.GRADIENT_SAVE_PATH_DEFAULT,*/ "Gradients");
			if (Directory.Exists(GlobalConfiguration.instance.GradientSavePath))
			{
				if (GUILayout.Button("Save Color Gradient Texture", GUILayout.MaxWidth(CommonStyles.BUTTON_WIDTH)))
				{
					EditorUtils.SaveTextureAsPNG(GlobalConfiguration.instance.GradientSavePath, "ColorGradient", "Gradient", GradientTex, GradientFiltering, 
						TextureImporterType.Default, TextureWrapMode.Clamp);
				}
			}
		}
	}
}
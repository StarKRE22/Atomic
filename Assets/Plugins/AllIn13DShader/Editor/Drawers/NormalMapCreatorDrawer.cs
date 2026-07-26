using System;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class NormalMapCreatorDrawer
	{
		private NormalMapCreatorTool normalMapCreatorTool;
		private CommonStyles commonStyles;
		private Action repaintAction;


		private Texture2D TargetNormalImage
		{
			get
			{
				return normalMapCreatorTool.targetNormalImage;
			}
			set
			{
				normalMapCreatorTool.targetNormalImage = value;
			}
		}

		private float NormalStrength
		{
			get
			{
				return normalMapCreatorTool.normalStrength;
			}
			set
			{
				normalMapCreatorTool.normalStrength = value;
			}
		}

		private int NormalSmoothing
		{
			get
			{
				return normalMapCreatorTool.normalSmoothing;
			}
			set
			{
				normalMapCreatorTool.normalSmoothing = value;
			}
		}

		private int IsComputingNormals
		{
			get
			{
				return normalMapCreatorTool.isComputingNormals;
			}
			set
			{
				normalMapCreatorTool.isComputingNormals = value;
			}
		}

		public NormalMapCreatorDrawer(NormalMapCreatorTool normalMapCreatorTool, CommonStyles commonStyles, Action repaintAction)
		{
			this.normalMapCreatorTool = normalMapCreatorTool;
			this.commonStyles = commonStyles;
			this.repaintAction = repaintAction;
		}

		public void Draw()
		{
			GUILayout.Label("Normal/Distortion Map Creator", commonStyles.bigLabel);
			GUILayout.Space(20);

			GUILayout.Label("Select the folder where new Normal Maps will be saved when the Create Normal Map button of the asset component is pressed", EditorStyles.boldLabel);

			GlobalConfiguration.instance.NormalMapSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.NormalMapSavePath, /*AllIn13DShaderConfig.NORMAL_MAP_SAVE_PATH_DEFAULT,*/ "Normal Maps Folder");

			GUILayout.Label("Assign a texture you want to create a normal map from. Choose the normal map settings and press the 'Create And Save Normal Map' button", EditorStyles.boldLabel);
			TargetNormalImage = (Texture2D)EditorGUILayout.ObjectField("Target Image", TargetNormalImage, typeof(Texture2D), false, GUILayout.MaxWidth(225));

			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Normal Strength:", GUILayout.MaxWidth(150));
				NormalStrength = EditorGUILayout.Slider(NormalStrength, 1f, 20f, GUILayout.MaxWidth(400));
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Normal Smoothing:", GUILayout.MaxWidth(150));
				NormalSmoothing = EditorGUILayout.IntSlider(NormalSmoothing, 0, 3, GUILayout.MaxWidth(400));
			}
			EditorGUILayout.EndHorizontal();

			if (IsComputingNormals == 0)
			{
				if (TargetNormalImage != null)
				{
					if (GUILayout.Button("Create And Save Normal Map", GUILayout.MaxWidth(CommonStyles.BUTTON_WIDTH)))
					{
						IsComputingNormals = 1;
						return;
					}
				}
				else
				{
					GUILayout.Label("Add a Target Image to use this feature", EditorStyles.boldLabel);
				}
			}
			else
			{
				GUILayout.Label("Normal Map is currently being created, be patient", EditorStyles.boldLabel, GUILayout.Height(40));
				repaintAction();

				IsComputingNormals++;
				if(IsComputingNormals > 5)
				{
					EditorUtils.SetTextureReadWrite(AssetDatabase.GetAssetPath(TargetNormalImage), true);

					Texture2D normalMapToSave = normalMapCreatorTool.CreateNormalMap();
					EditorUtils.SaveTextureAsPNG(GlobalConfiguration.instance.NormalMapSavePath, "NormalMap", "Normal Map", normalMapToSave, FilterMode.Bilinear, TextureImporterType.NormalMap, TextureWrapMode.Repeat);

					IsComputingNormals = 0;
				}
			}

			GUILayout.Label("*This process will freeze the editor for some seconds, larger images will take longer", EditorStyles.boldLabel);
		}
	}
}
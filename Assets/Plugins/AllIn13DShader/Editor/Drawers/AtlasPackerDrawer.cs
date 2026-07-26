using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class AtlasPackerDrawer
	{
		//public Texture2D[] Atlas;
		
		private AtlasPackerTool atlasPackerTool;
		//private AtlasPackerValues atlasPackerValues;

		public AtlasPackerValues ToolValues
		{
			get
			{
				return atlasPackerTool.values;
			}
		}



		private CommonStyles commonStyles;
		private SerializedObject soAtlasPackerValues;

		//private bool SquareAtlas
		//{
		//	get
		//	{
		//		return atlasPackerTool.squareAtlas;
		//	}
		//	set
		//	{
		//		atlasPackerTool.squareAtlas = value;
		//	}
		//}

		//private int AtlasXCount
		//{
		//	get
		//	{
		//		return atlasPackerTool.atlasXCount;
		//	}
		//	set
		//	{
		//		atlasPackerTool.atlasXCount = value;
		//	}
		//}

		//private int AtlasYCount
		//{
		//	get
		//	{
		//		return atlasPackerTool.atlasYCount;
		//	}
		//	set
		//	{
		//		atlasPackerTool.atlasYCount = value;
		//	}
		//}

		//private FilterMode AtlasFiltering
		//{
		//	get
		//	{
		//		return atlasPackerTool.atlasFiltering;
		//	}
		//	set
		//	{
		//		atlasPackerTool.atlasFiltering = value;
		//	}
		//}

		//private TextureSizes AtlasSizesX
		//{
		//	get
		//	{
		//		return atlasPackerTool.atlasSizesX;
		//	}
		//	set
		//	{
		//		atlasPackerTool.atlasSizesX = value;
		//	}
		//}

		//private TextureSizes AtlasSizesY
		//{
		//	get
		//	{
		//		return atlasPackerTool.atlasSizesY;
		//	}
		//	set
		//	{
		//		atlasPackerTool.atlasSizesY = value;
		//	}
		//}

		public AtlasPackerDrawer(AtlasPackerTool atlasPackerTool, CommonStyles commonStyles)
		{
			this.atlasPackerTool = atlasPackerTool;
			this.commonStyles = commonStyles;

			RefreshAtlasPackerValues();
			//atlasPackerValues = ScriptableObject.CreateInstance<AtlasPackerValues>();
			//atlasPackerValues.Atlas = new Texture2D[0];
		}

		//public void Setup(AtlasPackerTool atlasPackerTool, CommonStyles commonStyles)
		//{
		//	this.atlasPackerTool = atlasPackerTool;
		//	this.commonStyles = commonStyles;
		//	Atlas = new Texture2D[0];
		//}

		private void RefreshAtlasPackerValues()
		{
			//atlasPackerValues = ScriptableObject.CreateInstance<AtlasPackerValues>();
			//atlasPackerValues.
			soAtlasPackerValues = new SerializedObject(ToolValues);
		}

		public void Draw()
		{
			soAtlasPackerValues.Update();

			if (ToolValues == null)
			{
				RefreshAtlasPackerValues();
			}

			GUILayout.Label("Texture Atlas / Spritesheet Packer", commonStyles.bigLabel);
			GUILayout.Space(20);
			GUILayout.Label("Add Textures to the Atlas array", EditorStyles.boldLabel);


			SerializedProperty atlasProperty = soAtlasPackerValues.FindProperty("atlas");
			EditorGUILayout.PropertyField(atlasProperty, true, GUILayout.MaxWidth(200));
			soAtlasPackerValues.ApplyModifiedProperties();

			//ToolValues.atlas = Atlas;

			ToolValues.squareAtlas = EditorGUILayout.Toggle("Square Atlas?", ToolValues.squareAtlas, GUILayout.MaxWidth(200));
			EditorGUILayout.BeginHorizontal();
			{
				if (ToolValues.squareAtlas)
				{
					ToolValues.atlasXCount = EditorGUILayout.IntSlider("Column and Row Count", ToolValues.atlasXCount, 1, 8, GUILayout.MaxWidth(302));
					ToolValues.atlasYCount = ToolValues.atlasXCount;
				}
				else
				{
					ToolValues.atlasXCount = EditorGUILayout.IntSlider("Column Count", ToolValues.atlasXCount, 1, 8, GUILayout.MaxWidth(302));
					GUILayout.Space(10);
					ToolValues.atlasYCount = EditorGUILayout.IntSlider("Row Count", ToolValues.atlasYCount, 1, 8, GUILayout.MaxWidth(302));
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				if (ToolValues.squareAtlas)
				{
					GUILayout.Label("Atlas Size:", GUILayout.MaxWidth(100));
					ToolValues.atlasSizesX = (TextureSizes)EditorGUILayout.EnumPopup(ToolValues.atlasSizesX, GUILayout.MaxWidth(200));
					ToolValues.atlasSizesY = ToolValues.atlasSizesX;
				}
				else
				{
					GUILayout.Label("Atlas Size X:", GUILayout.MaxWidth(100));
					ToolValues.atlasSizesX = (TextureSizes)EditorGUILayout.EnumPopup(ToolValues.atlasSizesX, GUILayout.MaxWidth(200));
					GUILayout.Space(10);
					GUILayout.Label("Atlas Size Y:", GUILayout.MaxWidth(100));
					ToolValues.atlasSizesY = (TextureSizes)EditorGUILayout.EnumPopup(ToolValues.atlasSizesY, GUILayout.MaxWidth(200));
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Atlas Filtering: ", GUILayout.MaxWidth(100));
				ToolValues.atlasFiltering = (FilterMode)EditorGUILayout.EnumPopup(ToolValues.atlasFiltering, GUILayout.MaxWidth(200));
			}
			EditorGUILayout.EndHorizontal();

			int atlasElements = atlasPackerTool.GetAtlasElements();
			int atlasWidth = (int)ToolValues.atlasSizesX;
			int atlasHeight = (int)ToolValues.atlasSizesY;
			GUILayout.Label("Output will be a " + ToolValues.atlasXCount + " X " + ToolValues.atlasYCount + " atlas, " + atlasElements + " elements in total. In a " +
							atlasWidth + "pixels X " + atlasHeight + "pixels texture", EditorStyles.boldLabel);

			int usedAtlasSlots = 0;
			for (int i = 0; i < ToolValues.atlas.Length; i++)
			{
				if (ToolValues.atlas[i] != null)
				{
					usedAtlasSlots++;
				}
			}
			if (usedAtlasSlots > atlasElements)
			{
				GUILayout.Label("*Please reduce the Atlas texture slots by " + Mathf.Abs(atlasElements - ToolValues.atlas.Length) + " (extra textures will be ignored)", EditorStyles.boldLabel);
			}

			if (atlasElements > usedAtlasSlots)
			{
				GUILayout.Label("*" + (atlasElements - usedAtlasSlots) + " atlas slots unused or null (it will be filled with black)", EditorStyles.boldLabel);
			}

			GUILayout.Space(20);
			GUILayout.Label("Select the folder where new Atlases will be saved", EditorStyles.boldLabel);
			GlobalConfiguration.instance.AtlasesSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.AtlasesSavePath, /*AllIn13DShaderConfig.ATLASES_SAVE_PATH_DEFAULT,*/ "Atlas");


			if (Directory.Exists(GlobalConfiguration.instance.AtlasesSavePath))
			{
				if (GUILayout.Button("Create And Save Atlas Texture", GUILayout.MaxWidth(CommonStyles.BUTTON_WIDTH)))
				{
					atlasPackerTool.CreateAtlas();
					EditorUtils.SaveTextureAsPNG(GlobalConfiguration.instance.AtlasesSavePath, "Atlas", "Texture Atlas", ToolValues.createdAtlas, ToolValues.atlasFiltering, TextureImporterType.Default, TextureWrapMode.Clamp);
				}
			}
		}
	}
}
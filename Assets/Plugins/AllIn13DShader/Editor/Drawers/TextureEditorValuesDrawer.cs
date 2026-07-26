using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class TextureEditorValuesDrawer
	{
		private TextureEditorTool textureEditorTool;


		private TextureEditorValues values
		{
			get
			{
				return textureEditorTool.values;
			}
		}

		private Texture2D editorTexInput
		{
			get
			{
				return textureEditorTool.editorTexInput;
			}
		}

		private Texture2D editorTex
		{
			get
			{
				return textureEditorTool.editorTex;
			}
		}

		private Texture2D cleanEditorTex
		{
			get
			{
				return textureEditorTool.cleanEditorTex;
			}
		}


		private const int BUTTON_WIDTH = 600;


		public void Setup(TextureEditorTool textureEditorTool)
		{
			this.textureEditorTool = textureEditorTool;
		}

		public void Draw()
		{
			EditorGUILayout.BeginHorizontal();

			if (!values.showOriginalImage)
			{
				GUILayout.Label(editorTex);
			}
			else
			{
				GUILayout.Label(cleanEditorTex);
			}

			EditorGUILayout.BeginVertical();

			EditorGUI.BeginChangeCheck();
			EditorUtils.TextureEditorColorParameter("Color Tint", ref values.editorColorTint, Color.white);
			EditorUtils.TextureEditorFloatParameter("Brightness", ref values.brightness, -1f, 5f);
			EditorUtils.TextureEditorFloatParameter("Contrast", ref values.contrast, 0.0f, 5.0f, 1f);
			EditorUtils.TextureEditorFloatParameter("Gamma", ref values.gamma, 0.0f, 10f, 1f);
			EditorUtils.TextureEditorFloatParameter("Exposure", ref values.exposure, -5f, 5f, 0f);
			EditorUtils.TextureEditorFloatParameter("Saturation", ref values.saturation, 0f, 5f, 1f);
			EditorUtils.TextureEditorFloatParameter("Hue", ref values.hue, 0f, 360f, 0f);

			EditorGUILayout.BeginHorizontal();
			{
				values.invert = EditorGUILayout.Toggle("Invert", values.invert, GUILayout.Width(253));
				values.greyscale = EditorGUILayout.Toggle("Greyscale", values.greyscale);
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				values.fullWhite = EditorGUILayout.Toggle("Fully white", values.fullWhite, GUILayout.Width(253));
				values.blackBackground = EditorGUILayout.Toggle("Black background", values.blackBackground);
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			{
				values.alphaGreyscale = EditorGUILayout.Toggle("Greyscale is alpha", values.alphaGreyscale, GUILayout.Width(253));
				values.alphaIsOne = EditorGUILayout.Toggle("Alpha to 1", values.alphaIsOne);
			}
			EditorGUILayout.EndHorizontal();
			
			if (EditorGUI.EndChangeCheck())
			{
				textureEditorTool.RecalculateEditorTexture();
			}

			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Rotate Left 90°", GUILayout.MaxWidth(210)))
				{
					textureEditorTool.RotateEditorTextureLeft();
				}

				if (GUILayout.Button("Rotate Right 90°", GUILayout.MaxWidth(210)))
				{
					for (int i = 0; i < 3; i++)
					{
						textureEditorTool.RotateEditorTextureLeft();
					}
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Flip Horizontal", GUILayout.MaxWidth(210)))
				{
					textureEditorTool.FlipEditorTexture(true);
				}

				if (GUILayout.Button("Flip Vertical", GUILayout.MaxWidth(210)))
				{
					textureEditorTool.FlipEditorTexture(false);
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			if (!values.showOriginalImage)
			{
				if (GUILayout.Button("Press to show Original Image", GUILayout.MaxWidth(425)))
				{
					values.showOriginalImage = true;
				}
			}
			else
			{
				Color backgroundColor = GUI.backgroundColor;
				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("Press to show Editor Image", GUILayout.MaxWidth(425)))
				{
					values.showOriginalImage = false;
				}
				GUI.backgroundColor = backgroundColor;
			}


			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();

			GUILayout.Label("*Preview is locked to 256px maximum (bigger textures are scaled down), but the image will be saved to its full resolution", EditorStyles.boldLabel);

			EditorUtils.DrawThinLine();
			EditorGUILayout.Space();
			EditorUtils.TextureEditorFloatParameter("Export Scale", ref values.exportScale, 0.01f, 2f, 1f);
			int currWidth = Mathf.ClosestPowerOfTwo((int)(editorTexInput.width * values.exportScale));
			int currHeight = Mathf.ClosestPowerOfTwo((int)(editorTexInput.height * values.exportScale));
			GUILayout.Label("Current export size is: " + currWidth + " x " + currHeight + " (size snaps to the closest power of 2)", EditorStyles.boldLabel);

			if (GUILayout.Button("Save Resulting Image as PNG file", GUILayout.MaxWidth(BUTTON_WIDTH)))
			{
				textureEditorTool.SaveAsPNG();
			}
		}
	}
}
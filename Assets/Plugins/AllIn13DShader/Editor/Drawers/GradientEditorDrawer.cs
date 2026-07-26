using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class GradientEditorDrawer
	{
		private const float OFFSET = 5f;

		private Gradient gradient;
		private Rect lastRect;
		private Texture2D previewTex;
		private GradientCreatorTool gradientCreatorTool;

		private bool createGradientToggle;
		private bool textureSaved;
		private bool usingPreviewTex;
		private bool isModifyingExistingTexture;

		private Texture lastSavedTexture;
		private Gradient lastGradient;

		private GradientTexture gradientTextureAsset;

		private void RefreshReferences(Texture lastTexture)
		{
			if (gradient == null)
			{
				gradient = new Gradient();
			}

			if (previewTex == null)
			{
				previewTex = new Texture2D(128, 128);
			}

			if (gradientCreatorTool == null)
			{
				gradientCreatorTool = new GradientCreatorTool();
			}

			if (lastSavedTexture == null)
			{
				lastSavedTexture = lastTexture;
			}

			if (lastGradient == null)
			{
				lastGradient = new Gradient();
			}
		}

		private Texture RefreshGradientState(Texture texValue)
		{
			Texture res = texValue;

			if (createGradientToggle)
			{
				lastSavedTexture = texValue;

				isModifyingExistingTexture = EditorUtils.IsProjectAsset(lastSavedTexture);

				this.gradientTextureAsset = GradientCreatorTool.FindGradientTexureByTex(texValue);
				if (this.gradientTextureAsset != null)
				{
					GradientCreatorTool.CopyGradient(gradientTextureAsset.gradient, gradient);
				}
				else
				{
					if (isModifyingExistingTexture)
					{
						gradient = CreateGradientFromTexture(texValue);
					}
					else
					{
						gradient = new Gradient();
					}
				}

				previewTex = gradientCreatorTool.CreateGradientTexture(gradient);
				res = previewTex;
			}
			else
			{
				GradientCreatorTool.CopyGradient(gradient, lastGradient);
				previewTex = null;
				res = lastSavedTexture;
			}

			return res;
		}

		private Gradient CreateGradientFromTexture(Texture texSource)
		{
			int width = texSource.width;
			int height = texSource.height;

			RenderTexture renderTex = RenderTexture.GetTemporary(
				width,
				height,
				0,
				RenderTextureFormat.Default,
				RenderTextureReadWrite.Linear);

			Graphics.Blit(texSource, renderTex);
			RenderTexture previous = RenderTexture.active;
			RenderTexture.active = renderTex;
			Texture2D readableText = new Texture2D(texSource.width, texSource.height);
			readableText.filterMode = texSource.filterMode;
			readableText.wrapMode = texSource.wrapMode;
			readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
			readableText.Apply();
			RenderTexture.active = previous;
			RenderTexture.ReleaseTemporary(renderTex);


			Gradient res = new Gradient();
			List<GradientColorKey> gradientColorKeys = new List<GradientColorKey>();

			int numSamples = 5;
			float step = (((float)width) / numSamples) / width;

			Vector2 sampleUV = Vector2.zero;
			sampleUV.y = renderTex.height * 0.5f;
			for (int i = 0; i < numSamples; i++)
			{
				Color col = readableText.GetPixelBilinear(sampleUV.x, sampleUV.y);
				gradientColorKeys.Add(new GradientColorKey(col, sampleUV.x));

				sampleUV.x += step;
			}

			Color lastCol = readableText.GetPixelBilinear(sampleUV.x, sampleUV.y);
			gradientColorKeys.Add(new GradientColorKey(lastCol, sampleUV.x));

			res.colorKeys = gradientColorKeys.ToArray();
			res.mode = GradientMode.Blend;

			return res;
		}

		public Texture Draw(Rect position, Texture gradientTexture)
		{
			RefreshReferences(gradientTexture);

			lastRect = position;

			Texture texValue = gradientTexture;

			EditorGUI.BeginChangeCheck();
			
			if (createGradientToggle && texValue == null)
			{
				texValue = lastSavedTexture;
				createGradientToggle = false;
			}

			texValue = DrawTextureField(texValue, 60f, "Color ramp texture");
			bool changes = EditorGUI.EndChangeCheck();
			if (changes)
			{
				createGradientToggle = false;
				lastSavedTexture = texValue;
			}


			EditorGUI.BeginChangeCheck();
			DrawCreateGradientToggle(15f, "Create Gradient");
			bool createGradientToggleChanged = EditorGUI.EndChangeCheck();
			if (createGradientToggleChanged)
			{
				texValue = RefreshGradientState(texValue);
			}

			EditorGUI.BeginChangeCheck();
			if (createGradientToggle)
			{
				texValue = DrawGradientField(40f, "Gradient", texValue);
			}

			EditorGUI.showMixedValue = false;
			bool gradientFieldChanged = EditorGUI.EndChangeCheck();
			if (gradientFieldChanged && !textureSaved)
			{
				previewTex = gradientCreatorTool.CreateGradientTexture(TextureSizes._256, FilterMode.Bilinear, gradient);
				texValue = previewTex;

				usingPreviewTex = true;
			}

			textureSaved = false;

			return texValue;
		}

		private Texture DrawTextureField(Texture tex, float height, string label)
		{
			Rect rect = new Rect(lastRect);
			rect.height = height;

			GUIStyle labelStyle = EditorStyles.label;
			labelStyle.alignment = TextAnchor.UpperLeft;

			Rect labelRect = new Rect(rect);
			labelRect.width = 200f;
			EditorGUI.LabelField(labelRect, new GUIContent(label), labelStyle);

			GUIStyle textureStyle = EditorStyles.objectField;
			textureStyle.alignment = TextAnchor.MiddleLeft;

			Rect textureRect = new Rect(rect);
			textureRect.width = height;
			textureRect.height = height;
			textureRect.x = rect.width - textureRect.width - 10f;

			Texture res = (Texture)EditorGUI.ObjectField(textureRect, tex, typeof(Texture), false);
			lastRect.y += height;

			return res;
		}

		private Texture DrawGradientField(float height, string label, Texture texValue)
		{
			Texture res = texValue;

			Rect rect = new Rect(lastRect);
			rect.height = height;
			rect.y += height * 0.5f;

			float createButtonWidth = 100f;

			GUIStyle labelStyle = EditorStyles.label;
			labelStyle.alignment = TextAnchor.UpperLeft;
			Rect labelRect = new Rect(rect);
			labelRect.width = 200f;
			EditorGUI.LabelField(labelRect, new GUIContent(label), labelStyle);

			GUIStyle textureStyle = EditorStyles.objectField;
			textureStyle.alignment = TextAnchor.MiddleLeft;

			Rect gradientAndButtonRect = new Rect(rect);
			gradientAndButtonRect.width = rect.width - labelRect.width;
			gradientAndButtonRect.height = rect.height;
			gradientAndButtonRect.x = labelRect.x + labelRect.width;

			Rect gradientRect = new Rect(gradientAndButtonRect);
			gradientRect.width = gradientAndButtonRect.width * 0.5f;
			gradientRect.height = rect.height * 0.5f;
			gradientRect.x = rect.x + rect.width - gradientRect.width - (createButtonWidth * 2f) - 10f;
			gradient = EditorGUI.GradientField(gradientRect, gradient);

			Rect createButtonRect = new Rect(gradientAndButtonRect);
			createButtonRect.width = createButtonWidth;
			createButtonRect.height = gradientRect.height;
			createButtonRect.x = gradientRect.x + gradientRect.width + 10f;

			Rect modifyButtonRect = new Rect(createButtonRect);
			modifyButtonRect.x = createButtonRect.x + createButtonRect.width + 10f;

			if (GUI.Button(createButtonRect, "Save Texture"))
			{
				res = ChangeTextureOnDisk(true);
			}

			EditorGUI.BeginDisabledGroup(!isModifyingExistingTexture || gradientTextureAsset == null);
			if (GUI.Button(modifyButtonRect, "Overwrite"))
			{
				res = ChangeTextureOnDisk(false);
			}
			EditorGUI.EndDisabledGroup();

			lastRect.y += height + OFFSET;

			if (usingPreviewTex && !textureSaved)
			{
				Rect helpBoxRect = new Rect(lastRect);
				helpBoxRect.height = 40f;
				EditorGUI.HelpBox(helpBoxRect, "Texture is not saved! Click on Save Texture to save it", MessageType.Warning);

				lastRect.y += helpBoxRect.height + OFFSET;
			}

			return res;
		}

		private Texture ChangeTextureOnDisk(bool createNew)
		{
			gradientCreatorTool.CreateGradientTexture(gradient);
			Texture savedTex = gradientCreatorTool.SaveGradientTexture(gradientTextureAsset, createNew);

			Texture res = savedTex;
			lastSavedTexture = savedTex;

			textureSaved = true;
			usingPreviewTex = false;
			createGradientToggle = false;

			gradientTextureAsset = null;

			return res;
		}

		private void DrawCreateGradientToggle(float height, string label)
		{
			Rect rect = new Rect(lastRect);
			rect.height = height;

			Rect toggleRect = new Rect(rect);

			createGradientToggle = EditorGUI.ToggleLeft(toggleRect, label, createGradientToggle);
			
			lastRect.y += height;
		}

		public float GetPropertyHeight()
		{
			float res = 80f;

			if (createGradientToggle)
			{
				res += 45f;
				if (usingPreviewTex)
				{
					res += 40f;
				}
			}

			return res;
		}
	}
}
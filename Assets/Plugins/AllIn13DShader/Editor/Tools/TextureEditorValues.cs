using UnityEngine;

namespace AllIn13DShader
{
	public class TextureEditorValues
	{
		public Color editorColorTint;
		public float brightness;
		public float contrast;
		public float gamma;
		public float exposure;
		public float saturation;
		public float hue;
		public bool invert;
		public bool greyscale;
		public bool fullWhite;
		public bool blackBackground;
		public bool alphaGreyscale;
		public bool alphaIsOne;
		public bool showOriginalImage;
		public bool isFlipHorizontal;
		public bool isFlipVertical;
		public int rotationAmount;
		public float exportScale;

		public TextureEditorValues()
		{
			SetDefault();
		}

		public void SetDefault()
		{
			editorColorTint = Color.white;
			brightness = 0f;
			contrast = 1f;
			gamma = 1f;
			exposure = 0f;
			saturation = 1f;
			hue = 0f;
			invert = false;
			greyscale = false;
			fullWhite = false;
			blackBackground = false;
			alphaGreyscale = false;
			showOriginalImage = false;
			isFlipHorizontal = false;
			isFlipVertical = false;
			rotationAmount = 0;
			exportScale = 1f;
		}
	}
}
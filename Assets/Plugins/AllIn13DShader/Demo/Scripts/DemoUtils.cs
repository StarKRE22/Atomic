using UnityEngine;

namespace AllIn13DShader
{
	public static class DemoUtils
	{
		public static float EaseOutBack(float progress)
		{
			const float overshootBase = 2.7f;
			float overshootModifier = overshootBase + 1f;

			float res = 1f + overshootModifier * Mathf.Pow(progress - 1f, 3f) + overshootBase * Mathf.Pow(progress - 1f, 2f);
			return res;
		}

		public static void SetMaterialTransparent(Material mat)
		{
			mat.renderQueue = 3000;
			mat.SetFloat("_BlendSrc", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat.SetFloat("_BlendDst", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		}

		public static void SetMaterialOpaque(Material mat)
		{
			mat.renderQueue = 2000;
			mat.SetFloat("_BlendSrc", (float)UnityEngine.Rendering.BlendMode.One);
			mat.SetFloat("_BlendDst", (float)UnityEngine.Rendering.BlendMode.Zero);
		}
	}
}
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class ConverterURPLit : ConverterStandard
	{
		protected override void ConvertBlending()
		{
			if (from.IsKeywordEnabled("_SURFACE_TYPE_TRANSPARENT"))
			{
				if (from.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON"))
				{
					SetBlendSrc(BlendMode.One);
					SetBlendDst(BlendMode.OneMinusSrcAlpha);
				}
				else
				{
					SetBlendSrc(BlendMode.SrcAlpha);
					SetBlendDst(BlendMode.OneMinusSrcAlpha);
				}

				SetAlphaPreset();
			}
			else
			{
				SetBlendSrc(BlendMode.One);
				SetBlendDst(BlendMode.Zero);
				SetOpaquePreset();
			}
		}
	}
}
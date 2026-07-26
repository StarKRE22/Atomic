using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class ConverterStandardBIRP : ConverterStandard
	{
		protected override void ConvertBlending()
		{
			if (from.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON"))
			{
				SetBlendSrc(BlendMode.One);
				SetBlendDst(BlendMode.OneMinusSrcAlpha);
				SetAlphaPreset();
			}
			else if (from.IsKeywordEnabled("_ALPHABLEND_ON"))
			{
				SetBlendSrc(BlendMode.SrcAlpha);
				SetBlendDst(BlendMode.OneMinusSrcAlpha);
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
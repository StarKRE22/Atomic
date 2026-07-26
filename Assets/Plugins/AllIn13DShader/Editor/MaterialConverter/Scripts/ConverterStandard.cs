using UnityEngine;
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class ConverterStandard : ConverterGeneral
	{
		public override void ApplyConversion(Material from, Material target)
		{
			base.ApplyConversion(from, target);

			DisableEffect("ALPHA_CUTOFF");

			bool emissionEffectEnabled = from.IsKeywordEnabled("_EMISSION");
			SetEnableEffect("EMISSION", emissionEffectEnabled);

			bool normalMapEffectEnabled = from.IsKeywordEnabled("_NORMALMAP");
			SetEnableEffect("NORMAL_MAP", normalMapEffectEnabled);

			EnableLightModelClassic();

			EnablePBR();
			
			target.SetFloat("_ReflectionsAtten", 1.0f);

			bool specularEnabled = !from.IsKeywordEnabled("_SPECULARHIGHLIGHTS_OFF");
			if (specularEnabled)
			{
				EnableSpecularClassic();
				target.SetFloat("_SpecularAtten", 1.0f);
			}
			else
			{
				DisableEffect("SPECULARMODEL");
			}

			DisableEffect("RIM_LIGHTING");

			//Blending Mode
			ConvertBlending();
		}

		protected virtual void ConvertBlending()
		{
		
		}
	}
}
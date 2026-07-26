using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class ShadowsConfigurator : MonoBehaviour
	{
		public Color shadowColor;

		private readonly int shadowColorPropID = Shader.PropertyToID("global_shadowColor");

#if UNITY_EDITOR
		public void Update()
		{
			SetupShadowColor();
		}
#endif

		public void SetupShadowColor()
		{
			Shader.SetGlobalColor(shadowColorPropID, shadowColor);
		}
	}
}
using UnityEngine;

namespace AllIn13DShader
{
	public class URPSettingsDrawer
	{
		private CommonStyles commonStyles;

		public void Setup(CommonStyles commonStyles)
		{
			this.commonStyles = commonStyles;
		}

		public void Draw()
		{
			GUILayout.Label("Configure AllIn13D to work correctly with URP", commonStyles.bigLabel);
			if (GUILayout.Button("Configure"))
			{
#if ALLIN13DSHADER_URP
				URPConfigurator.Configure();
#endif
			}
		}
	}
}
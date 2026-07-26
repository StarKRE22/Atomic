using UnityEditor;

namespace AllIn13DShader
{
	[CustomEditor(typeof(AllIn1DepthColoringProperties))]
	public class AllIn1DepthColoringPropertiesCustomEditor : Editor
	{
		private AllIn1DepthColoringProperties depthColoringProperties;

		private DepthColoringPropertiesDrawer drawer;

		private void RefreshDrawer()
		{
			depthColoringProperties = (AllIn1DepthColoringProperties)target;

			if (drawer == null)
			{
				drawer = new DepthColoringPropertiesDrawer(depthColoringProperties);
			}
		}

		public override void OnInspectorGUI()
		{
			RefreshDrawer();

			drawer.Draw(false);
		}
	}
}
using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class DepthColoringCamera : MonoBehaviour
	{
		public Camera cam;
		public AllIn1DepthColoringProperties depthColoringProperties;

		private void OnEnable()
		{
			if (Application.isPlaying)
			{
				if (cam != null)
				{
					cam.depthTextureMode = DepthTextureMode.Depth;
				}

				depthColoringProperties.ApplyValues();
			}
		}

#if UNITY_EDITOR
		private void Update()
		{
			Update_Editor();
		}

		private void Reset()
		{
			cam = GetComponent<Camera>();
		}

		private void Update_Editor()
		{
			if(cam != null)
			{
				cam.depthTextureMode = DepthTextureMode.Depth;
			}
		}
#endif
	}
}
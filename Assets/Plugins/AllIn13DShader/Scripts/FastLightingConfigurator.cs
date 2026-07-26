using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class FastLightingConfigurator : MonoBehaviour
	{
		private int globalLightDirectionPropID = Shader.PropertyToID("global_lightDirection");

		public Vector3 globalLightDirection;

		private void Update()
		{
			Shader.SetGlobalVector(globalLightDirectionPropID, globalLightDirection.normalized);
		}
	}
}
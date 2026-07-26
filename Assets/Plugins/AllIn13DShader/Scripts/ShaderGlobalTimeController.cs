using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class ShaderGlobalTimeController : MonoBehaviour
	{
		private readonly int globalTimePropertyID = Shader.PropertyToID(ConstantsRuntime.GLOBAL_PROPERTY_GLOBAL_TIME);
		private Vector4 timeVector;

		private void Update()
		{
			timeVector.x = Time.unscaledTime / 20f;
			timeVector.y = Time.unscaledTime;
			timeVector.z = Time.unscaledTime * 2f;
			timeVector.w = Time.unscaledTime * 3f;
			Shader.SetGlobalVector(globalTimePropertyID, timeVector);
		}
	}
}
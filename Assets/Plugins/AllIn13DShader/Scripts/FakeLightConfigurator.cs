using System;
using UnityEngine;

namespace AllIn13DShader
{
	[ExecuteInEditMode]
	public class FakeLightConfigurator : MonoBehaviour
	{
		private readonly int global_lightDirection = Shader.PropertyToID("global_lightDirection");
		private readonly int global_lightColor = Shader.PropertyToID("global_lightColor");

		public Color lightColor = Color.white;

		private void Update()
		{
			Shader.SetGlobalVector(global_lightDirection, -transform.forward);
			Shader.SetGlobalColor(global_lightColor, lightColor);
		}

		private void OnDrawGizmosSelected()
		{
			Vector3 initPos = transform.position;
			Vector3 endPos = initPos + transform.forward * 5f;

			Gizmos.DrawLine(initPos, endPos);
			Gizmos.DrawSphere(endPos, 0.5f);
		}

		private void Reset()
		{
			Light thisLight = GetComponent<Light>();
			if(thisLight != null) lightColor = thisLight.color;
		}
	}
}
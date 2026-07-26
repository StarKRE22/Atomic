using UnityEngine;

namespace AllIn13DShader
{
	public class EnvironmentSpotlightController : EnvironmentController
	{
		public Transform lightsParent;
		public float rotationSpeed;

		protected override void Update()
		{
			base.Update();

			Quaternion quat = lightsParent.rotation;
			lightsParent.rotation = quat * Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, lightsParent.up);
		}
	}
}
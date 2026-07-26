using UnityEngine;

namespace AllIn13DShader
{
	public class DemoSceneConfiguration : ScriptableObject
	{
		public float scaleDuration = 0.4f;
		public float distanceBetweenDemos = 25f;
		public float cameraTransitionDuration = 1f;
		public float alphaDuration = 0.25f;

		[Header("Curves")]
		public AnimationCurve scalingCurve;
		public AnimationCurve cameraCurve;
		public AnimationCurve alphaCurve;

		[Header("Expositors")]
		public DemoExpositorData[] expositors;
	}
}
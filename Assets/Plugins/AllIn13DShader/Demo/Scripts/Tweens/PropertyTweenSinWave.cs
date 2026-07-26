using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class PropertyTweenSinWave : PropertyTween
	{
		[Header("Configuration")]
		public float minValue;
		public float maxValue;
		public float speed;

		protected override void Tween(float deltaTime)
		{
			float sin01 = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;

			currentValue = Mathf.Lerp(minValue, maxValue, sin01);
		}
	}
}
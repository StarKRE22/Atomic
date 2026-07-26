using UnityEngine;

namespace AllIn13DShader
{
	public class DemoElementIntersectionGlow : DemoElement
	{
		private float timer;

		public float speed;
		public float minY;
		public float maxY;

		public Transform target;

		private void OnEnable()
		{
			timer = 0f;
		}

		private void OnDisable()
		{
			timer = 0f;
		}

		public override void Update()
		{
			base.Update();

			timer += Time.deltaTime;

			float sin01 = (Mathf.Sin(timer * speed) + 1f) * 0.5f;
			Vector3 localPos = target.localPosition;
			localPos.y = Mathf.Lerp(minY, maxY, sin01);

			target.localPosition = localPos;
		}
	}
}
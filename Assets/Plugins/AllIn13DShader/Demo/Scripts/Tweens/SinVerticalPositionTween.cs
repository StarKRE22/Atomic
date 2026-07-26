using System;
using UnityEngine;

namespace AllIn13DShader
{
	public class SinVerticalPositionTween : MonoBehaviour
	{
		private float timer;

		public float speed = 5;
		public float minY = -1;
		public float maxY = 1;

		public Transform target;

		private void OnEnable()
		{
			timer = 0f;
		}

		private void OnDisable()
		{
			timer = 0f;
		}

		public void Update()
		{
			timer += Time.deltaTime;

			float sin01 = (Mathf.Sin(timer * speed) + 1f) * 0.5f;
			Vector3 localPos = target.localPosition;
			localPos.y = Mathf.Lerp(minY, maxY, sin01);

			target.localPosition = localPos;
		}

		private void Reset()
		{
			if(target == null) target = transform;
		}
	}
}
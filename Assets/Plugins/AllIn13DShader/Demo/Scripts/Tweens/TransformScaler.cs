using System;
using UnityEngine;

namespace AllIn13DShader
{
	public class TransformScaler : MonoBehaviour
	{
		public enum ScalingType
		{
			NONE,
			SCALING_UP,
			SCALING_DOWN,
		}

		private float scaleDuration;
		private AnimationCurve scaleCurve;

		private Vector3 localScaleSrc;
		private Vector3 localScaleDst;

		private float timer;
		private ScalingType scalingType;

		private Action<ScalingType> scaleFinishedCallback;

		public void Init(DemoSceneConfiguration demoSceneConfig)
		{
			Init(demoSceneConfig, null);
		}

		public void Init(DemoSceneConfiguration demoSceneConfig, Action<ScalingType> scaleFinishedCallback)
		{
			scaleDuration = demoSceneConfig.scaleDuration;
			scaleCurve = demoSceneConfig.scalingCurve;

			this.scaleFinishedCallback = scaleFinishedCallback;
		}

		public void ScaleUp()
		{
			transform.localScale = Vector3.zero;
			timer = 0f;
			scalingType = ScalingType.SCALING_UP;

			localScaleSrc = Vector3.zero;
			localScaleDst = Vector3.one;
		}

		public void ScaleDown()
		{
			transform.localScale = Vector3.one;
			timer = 0f;
			scalingType = ScalingType.SCALING_DOWN;

			localScaleSrc = Vector3.one;
			localScaleDst = Vector3.zero;
		}

		private void Update()
		{
			if (IsScaling())
			{
				UpdateScaling();
			}
		}

		private void UpdateScaling()
		{
			timer += Time.deltaTime;
			float t = timer / scaleDuration;
			float curveT = t;

			if (scalingType == ScalingType.SCALING_UP)
			{
				curveT = scaleCurve.Evaluate(t);
			}
			else if (scalingType == ScalingType.SCALING_DOWN)
			{
				curveT = 1 - scaleCurve.Evaluate(1 - t);
			}

			float scale = Mathf.LerpUnclamped(localScaleSrc.x, localScaleDst.x, curveT);
			scale = Mathf.Max(0f, scale);

			transform.localScale = Vector3.one * scale;

			if (t >= 1f)
			{
				timer = 0f;

				if (scalingType == ScalingType.SCALING_DOWN)
				{
					gameObject.SetActive(false);
				}

				if(scaleFinishedCallback != null)
				{
					scaleFinishedCallback(scalingType);
				}

				scalingType = ScalingType.NONE;
			}
		}

		public bool IsScaling()
		{
			return scalingType != ScalingType.NONE;
		}
	}
}
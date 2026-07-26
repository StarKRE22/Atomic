using UnityEngine;

namespace AllIn13DShader
{
	public class AlphaTween : MonoBehaviour
	{
		private static int MATPROP_ID_GENERAL_ALPHA = Shader.PropertyToID("_GeneralAlpha");

		public enum State
		{
			NONE = 0,
			FADE_IN = 1,
			FADE_OUT = 2,
		}

		private MeshRenderer[] meshRenderers;

		private bool IsTweening
		{
			get
			{
				bool res = state != State.NONE;
				return res;
			}
		}

		private AnimationCurve alphaCurve;
		private float duration;
		private float timer;

		private State state;

		private float alphaSrc;
		private float alphaDst;

		public void Init(DemoSceneConfiguration demoSceneConfig, MeshRenderer[] meshRenderers)
		{
			alphaCurve = demoSceneConfig.alphaCurve;
			duration = demoSceneConfig.alphaDuration;
			timer = 0f;

			this.meshRenderers = meshRenderers;

			state = State.NONE;
		}

		public void FadeIn()
		{
			timer = 0f;
			state = State.FADE_IN;

			alphaSrc = 0f;
			alphaDst = 1f;
		}

		public void FadeOut()
		{
			timer = 0f;
			state = State.FADE_OUT;

			alphaSrc = 1f;
			alphaDst = 0f;
		}

		public void Update()
		{
			if (IsTweening)
			{
				UpdateTweening();
			}
		}

		private void UpdateTweening()
		{
			timer += Time.deltaTime;
			float t = timer / duration;
			float curveT = alphaCurve.Evaluate(t);

			float alpha = Mathf.Lerp(alphaSrc, alphaDst, curveT);

			for(int i = 0; i < meshRenderers.Length; i++)
			{
				meshRenderers[i].sharedMaterial.SetFloat(MATPROP_ID_GENERAL_ALPHA, alpha);
			}

			if(t >= 1f)
			{
				state = State.NONE;
				timer = 0f;
			}
		}
	}
}
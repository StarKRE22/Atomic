using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class PropertyTweenFader : PropertyTween
	{
		public enum State
		{
			WAITING_FOR_FADE_IN = 0,
			FADING_IN = 1,
			WAITING_FOR_FADE_OUT = 2,
			FADING_OUT = 3,
		}

		private float timer;
		private State state;
		
		[Header("Config")]
		public float waitingForFadeInTime;
		public float fadeInDuration;
		public float waitingForFadeOutTime;
		public float fadeOutDuration;

		public override void Init(Material mat)
		{
			base.Init(mat);

			state = State.WAITING_FOR_FADE_IN;
			timer = 0f;
		}

		protected override void Tween(float deltaTime)
		{
			switch (state)
			{
				case State.WAITING_FOR_FADE_IN:
					UpdateWaitingForFadeIn();
					break;
				case State.FADING_IN:
					UpdateFadingIn();
					break;
				case State.WAITING_FOR_FADE_OUT:
					UpdateWaitingForFadeOut();
					break;
				case State.FADING_OUT:
					UpdateFadingOut();
					break;
			}
		}

		private void UpdateWaitingForFadeIn()
		{
			timer += Time.deltaTime;

			if (timer >= waitingForFadeInTime)
			{
				state = State.FADING_IN;
				timer = 0f;
			}
		}

		private void UpdateFadingIn()
		{
			timer += Time.deltaTime;

			float t = timer / fadeInDuration;
			currentValue = t;

			if (timer >= fadeInDuration)
			{
				state = State.WAITING_FOR_FADE_OUT;
				timer = 0f;
			}
		}

		private void UpdateWaitingForFadeOut()
		{
			timer += Time.deltaTime;

			if (timer >= waitingForFadeOutTime)
			{
				state = State.FADING_OUT;
				timer = 0f;
			}
		}

		private void UpdateFadingOut()
		{
			timer += Time.deltaTime;

			float t = timer / fadeOutDuration;
			currentValue = 1 - t;

			if (timer >= fadeOutDuration)
			{
				state = State.WAITING_FOR_FADE_IN;
				timer = 0f;
			}
		}
	}
}
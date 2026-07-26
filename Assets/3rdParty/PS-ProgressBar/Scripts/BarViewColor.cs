using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(Graphic))]
    public class BarViewColor : ProgressBarProView {
        
        [SerializeField] protected Graphic graphic;

		[Header("Color Options")]
		[Tooltip("The default color of the bar can be set by the ProgressBar.SetbarColor()")]
		[SerializeField] bool canOverrideColor;

		[SerializeField] Color defaultColor = Color.white;
        [Tooltip("Change color of the bar automatically based on it's value.")]
        [SerializeField] bool useGradient;
		[SerializeField] Gradient barGradient;

		private Color flashColor;
		private float flashcolorAlpha = 0f;
		private float currentValue;

        [Header("Color Animation")]
        [SerializeField] bool flashOnGain;
        [SerializeField] Color gainColor = Color.white;
        [SerializeField] bool flashOnLoss;
        [SerializeField] Color lossColor = Color.white;
        [SerializeField] float flashTime = 0.2f;

        private Coroutine colorAnim;

		void OnEnable() {
            flashcolorAlpha = 0f;
            UpdateColor();
		}

        public override void NewChangeStarted(float currentValue, float targetValue) {
            if (!flashOnGain && !flashOnLoss)
                return;
            else if (targetValue > currentValue && !flashOnGain)
                return;
            else if (targetValue < currentValue && !flashOnLoss)
                return;
			else if (gameObject.activeInHierarchy == false)
				return; // No Coroutine if we're disabled

            if (colorAnim != null)
                StopCoroutine(colorAnim);
                        
            colorAnim = StartCoroutine(DoBarColorAnim((targetValue < currentValue? lossColor : gainColor), flashTime));
        }

        IEnumerator DoBarColorAnim(Color targetColor, float duration) {
            float time = 0f;

            while (time < duration) {
				SetOverrideColor(targetColor, Utils.EaseSinInOut(time / duration, 1f, -1f));
				UpdateColor();
                time += Time.deltaTime;
                yield return null;
            }

			SetOverrideColor(targetColor, 0f);
			UpdateColor();
            colorAnim = null;
        }

        public override void SetBarColor(Color color) {
			if (!canOverrideColor)
				return;
			
			defaultColor = color;
			useGradient = false;

            if (colorAnim == null)
                UpdateColor();
        }

		private void SetOverrideColor(Color color, float alpha) {
			flashColor = color;
			flashcolorAlpha = alpha;
		}

		public override void UpdateView(float currentValue, float targetValue) {
			this.currentValue = currentValue;

            if (colorAnim == null) // if we're flashing don't update this since the coroutine handles our updates
				UpdateColor();
        }

		void UpdateColor() {
			graphic.canvasRenderer.SetColor( GetCurrentColor(currentValue) );
		}

        Color GetCurrentColor(float percentage) {
            if (flashcolorAlpha >= 1f)
                return flashColor;
            else if (flashcolorAlpha <= 0f)
				return useGradient ? barGradient.Evaluate(percentage) : defaultColor;
            else
				return Color.Lerp(useGradient ? barGradient.Evaluate(percentage) : defaultColor, flashColor, flashcolorAlpha);
        }

        #if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();

            graphic = GetComponent<Graphic>();
        }

		void OnValidate() {
			UpdateColor();
		}
        #endif
    }

}
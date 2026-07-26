using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(RectTransform))]
    public class BarViewScale : ProgressBarProView {

        [SerializeField] protected RectTransform graphic;

        [Header("Color Options")]
        [Tooltip("If true, then the scale animates for each change. Otherwise it scales constantly based on value")]
        [SerializeField] bool animateOnChange = true;

        [SerializeField] Vector3 minSize = Vector3.one;
        [SerializeField] Vector3 maxSize = new Vector3(2f, 2f, 2f);

        [Tooltip("A value of 0 is minSize, a value of 1 is maxSize. Time goes from 0 to 1.")]
        [SerializeField] AnimationCurve scale;
        [SerializeField] float animDuration = 0.2f;

        private Coroutine scaleAnim;

        void OnEnable() {
            UpdateScale();
        }

        public override void NewChangeStarted(float currentValue, float targetValue) {
            if (gameObject.activeInHierarchy == false || !animateOnChange)
                return; // No Coroutine if we're disabled

            if (scaleAnim != null)
                StopCoroutine(scaleAnim);

            scaleAnim = StartCoroutine(DoBarScaleAnim(animDuration));
        }

        IEnumerator DoBarScaleAnim(float duration) {
            float time = 0f;

            while (time < duration) {
                UpdateScale(time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            UpdateScale(0f);
            scaleAnim = null;
        }

        public override void UpdateView(float currentValue, float targetValue) {
            if (animateOnChange)
                return;

            if (scaleAnim == null) // if we're flashing don't update this since the coroutine handles our updates
                UpdateScale(currentValue);
        }

        void UpdateScale(float value = 0f) {
            graphic.localScale = GetCurrentScale(value);
        }

        Vector3 GetCurrentScale(float percentage) {
            return Vector3.Lerp(minSize, maxSize, scale.Evaluate(percentage));
        }

#if UNITY_EDITOR
        protected override void Reset() {
            base.Reset();

            graphic = GetComponent<RectTransform>();
        }

        void OnValidate() {
            UpdateScale();
        }
#endif
    }

}
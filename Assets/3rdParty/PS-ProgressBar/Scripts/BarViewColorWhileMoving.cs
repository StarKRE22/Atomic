using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(Graphic))]
    public class BarViewColorWhileMoving : ProgressBarProView {

        [SerializeField]
        protected Graphic graphic;

        [SerializeField] Color colorStatic = Color.white;
        [SerializeField] Color colorMoving = Color.blue;
        [SerializeField] float blendTimeOnMove = 0.2f;
        [SerializeField] float blendTimeOnStop = 0.05f;

        private bool isMoving = false;

        void OnEnable() {
            SetDefaultColor();
        }

        public override void UpdateView(float currentValue, float targetValue) {
            bool isNotAtTarget = (currentValue != targetValue);

            if (isMoving == isNotAtTarget)
                return;

            isMoving = isNotAtTarget;
            graphic.CrossFadeColor(GetCurrentColor(), isMoving ? blendTimeOnMove : blendTimeOnStop, false, true);
        }

        Color GetCurrentColor() {
             return isMoving ? colorMoving : colorStatic;
        }

        void SetDefaultColor() {
            graphic.canvasRenderer.SetColor(GetCurrentColor());
        }

#if UNITY_EDITOR
        protected override void Reset() {
            base.Reset();

            graphic = GetComponent<Graphic>();
        }

        void OnValidate() {
            SetDefaultColor();
        }
#endif
    }

}
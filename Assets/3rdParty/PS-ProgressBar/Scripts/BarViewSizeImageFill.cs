using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(Image))]
    public class BarViewSizeImageFill : ProgressBarProView {
        
        [SerializeField] protected Image image;
		[SerializeField] bool hideOnEmpty = true;
        [SerializeField] bool useDiscreteSteps = false;
        [SerializeField] int numSteps = 10;

        private bool isDisplaySizeZero;

        public override bool CanUpdateView(float currentValue, float targetValue) {
            // This ensures that we can update, even if the object has been updated cause it was set to 0
            return isActiveAndEnabled || isDisplaySizeZero;
        }

        public override void UpdateView(float currentValue, float targetValue) {
            if (hideOnEmpty && currentValue <= 0f) {
                image.gameObject.SetActive(false);
                isDisplaySizeZero = true;
                return;
            }

            isDisplaySizeZero = false;
            image.gameObject.SetActive(true);
			image.fillAmount = GetDisplayValue(currentValue);
        }

        float GetDisplayValue(float display) {
            if (!useDiscreteSteps)
                return display;

            return Mathf.Round(display * numSteps) / numSteps;
        }

#if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();
            image = GetComponent<Image>();
        }
#endif
    }

}
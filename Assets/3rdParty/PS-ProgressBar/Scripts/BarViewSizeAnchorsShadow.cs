using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(Image))]
    public class BarViewSizeAnchorsShadow : BarViewSizeAnchors {

        public enum ShadowType { Gaining, Losing }

        [SerializeField] ShadowType shadowType;

		public override void UpdateView(float currentValue, float targetValue) {
            if (Mathf.Approximately(currentValue, targetValue) 
                || (shadowType == ShadowType.Gaining && targetValue < currentValue) 
                || (shadowType == ShadowType.Losing && targetValue > currentValue) ) 
            {
                rectTrans.gameObject.SetActive(false);
                isDisplaySizeZero = true;
                return;
            }

            isDisplaySizeZero = false;
            rectTrans.gameObject.SetActive(true);

            if (shadowType == ShadowType.Gaining) 
                SetPivot(0f, targetValue);
            else
                SetPivot(targetValue, currentValue);
        }

    }
}
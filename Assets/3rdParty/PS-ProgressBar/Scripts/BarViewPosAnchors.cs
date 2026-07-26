using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    public class BarViewPosAnchors : BarViewSizeAnchors {

		public override void UpdateView(float currentValue, float targetValue) {
			SetPivot(currentValue, currentValue);
        }
    }
}
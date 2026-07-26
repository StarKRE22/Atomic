using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar {
[RequireComponent(typeof(RectTransform))]
public class BarViewPosImageFill : ProgressBarProView {

		[SerializeField] RectTransform rectTrans;
		[SerializeField] Image referenceImage;
		[Range(-1f, 1f)]
		[SerializeField] float offset = 0f;
        
        public override void UpdateView(float currentValue, float targetValue) {
			rectTrans.anchorMin = GetAnchor(currentValue);
			rectTrans.anchorMax = GetAnchor(currentValue);
		}

		Vector2 GetAnchor(float currentDisplay) {
			switch (referenceImage.fillMethod) {
			case Image.FillMethod.Horizontal:
				return GetAnchorHorizontal(currentDisplay, referenceImage.fillOrigin);
			case Image.FillMethod.Vertical:
				return GetAnchorVertical(currentDisplay, referenceImage.fillOrigin);
			case Image.FillMethod.Radial360:
				return GetAnchorRadial360(currentDisplay, referenceImage.fillOrigin, referenceImage.fillClockwise);
			case Image.FillMethod.Radial90:
			case Image.FillMethod.Radial180:
			default:
				return Vector2.one * 0.5f;
			}
		}

		Vector2 GetAnchorHorizontal(float fillAmount, int fillOrigin) {
			float x;

			if (fillOrigin == 1)
				x = 1 - fillAmount;
            else
				x = fillAmount;

			return new Vector2(x, 0.5f + 0.5f * offset);
		}

		Vector2 GetAnchorVertical(float fillAmount, int fillOrigin) {
			float y;

			if (fillOrigin == 1)
				y = 1 - fillAmount;
			else
				y = fillAmount;

			return new Vector2(0.5f + 0.5f * offset, y);
		}

		Vector2 GetAnchorRadial360(float fillAmount, int fillOrigin, bool fillClockwise ) {
			float degrees = 360f * (fillClockwise ? -fillAmount : fillAmount);
			
			if (fillOrigin == (int) Image.Origin360.Bottom)
				degrees += fillClockwise ? -90f : 90f;
			else if (fillOrigin == (int) Image.Origin360.Left)
				degrees += fillClockwise ? -180f : 180f;
			else if (fillOrigin == (int) Image.Origin360.Top)
				degrees += fillClockwise ? -270f : 270f;

			return GetPointOnCircle(degrees);
		}

		Vector2 GetPointOnCircle(float degrees) {
			float radius = 0.25f + 0.25f * offset;
			return new Vector2( 0.5f + radius * Mathf.Cos(Mathf.Deg2Rad * degrees), 0.5f + radius * Mathf.Sin(Mathf.Deg2Rad * degrees));
		}

		#if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();
			rectTrans = GetComponent<RectTransform>();
		}
		#endif
	}

}

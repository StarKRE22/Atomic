using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
	[RequireComponent(typeof(RectTransform))]
    public class BarViewSizeAnchors : ProgressBarProView {

        public enum FillType { LeftToRight, RightToLeft, TopToBottom, BottomToTop };

        [SerializeField] protected RectTransform rectTrans;
        [SerializeField] protected FillType fillType = FillType.LeftToRight;
        [SerializeField] bool hideOnEmpty = true;
        [SerializeField] bool useDiscreteSteps = false;
        [SerializeField] int numSteps = 10;

        protected DrivenRectTransformTracker m_Tracker;
        protected bool isDisplaySizeZero;

        public override bool CanUpdateView(float currentValue, float targetValue) {
            // This ensures that we can update, even if the object has been updated cause it was set to 0
            return isActiveAndEnabled || isDisplaySizeZero;
        }

        public override void UpdateView(float currentValue, float targetValue) {
			if (hideOnEmpty && currentValue <= 0f) {
                rectTrans.gameObject.SetActive(false);
                isDisplaySizeZero = true;
                return;
            }

            isDisplaySizeZero = false;
            rectTrans.gameObject.SetActive(true);
			SetPivot(0f, currentValue);
        }

        protected void SetPivot(float startEdge, float endEdge) {
            if (useDiscreteSteps) {
                startEdge = Mathf.Round(startEdge * numSteps) / numSteps;
                endEdge = Mathf.Round(endEdge * numSteps) / numSteps;
            }

            UpdateTracker();

            switch (fillType) {
                case FillType.LeftToRight:
                    rectTrans.anchorMin = new Vector2(startEdge, rectTrans.anchorMin.y);
                    rectTrans.anchorMax = new Vector2(endEdge, rectTrans.anchorMax.y);
                    break;
                case FillType.RightToLeft:
                    rectTrans.anchorMin = new Vector2(1f - endEdge, rectTrans.anchorMin.y);
                    rectTrans.anchorMax = new Vector2(1f - startEdge, rectTrans.anchorMax.y);
                    break;
                case FillType.BottomToTop:
                    rectTrans.anchorMin = new Vector2(rectTrans.anchorMin.x, startEdge);
                    rectTrans.anchorMax = new Vector2(rectTrans.anchorMax.x, endEdge);
                    break;
                case FillType.TopToBottom:
					rectTrans.anchorMin = new Vector2(rectTrans.anchorMin.x, 1f - endEdge);
                    rectTrans.anchorMax = new Vector2(rectTrans.anchorMax.x, 1f - startEdge);
                    break;
            }
        }

        void UpdateTracker() {
            if (fillType == FillType.LeftToRight || fillType == FillType.RightToLeft)
                m_Tracker.Add(this, rectTrans, DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX);
            else
                m_Tracker.Add(this, rectTrans, DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY);
        }

        void OnDisable() {
            m_Tracker.Clear();
        }

#if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();
            rectTrans = GetComponent<RectTransform>();
        }
#endif
    }

}
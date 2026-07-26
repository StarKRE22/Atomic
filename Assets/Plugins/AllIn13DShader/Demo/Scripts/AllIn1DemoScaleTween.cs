using UnityEngine;

namespace AllIn13DShader
{
    public class AllIn1DemoScaleTween : MonoBehaviour
    {
        [SerializeField] private float maxTweenScale = 1.25f;
        [SerializeField] private float minTweenScale = 0.5f;
        [SerializeField] private float tweenSpeed = 15f;
        
        private bool isTweening = false;
        private float currentScale = 1f;
        private Vector3 scaleToApply = Vector3.one;

        private void Update()
        {
            if(!isTweening) return;
            currentScale = Mathf.Lerp(currentScale, 1f, Time.unscaledDeltaTime * tweenSpeed);
            UpdateScaleToApply();
            ApplyScale();
            if(Mathf.Abs(currentScale - 1f) < 0.02f) isTweening = false;
        }

        private void UpdateScaleToApply()
        {
            scaleToApply.x = currentScale;
            scaleToApply.y = currentScale;
        }
        
        private void ApplyScale()
        {
            transform.localScale = scaleToApply;
        }

        public void ScaleUpTween()
        {
            isTweening = true;
            currentScale = maxTweenScale;
            UpdateScaleToApply();
        }
        
        public void ScaleDownTween()
        {
            isTweening = true;
            currentScale = minTweenScale;
            UpdateScaleToApply();
        }
    }
}
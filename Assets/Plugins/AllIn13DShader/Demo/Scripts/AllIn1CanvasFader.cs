using UnityEngine;

namespace AllIn13DShader
{
    public class AllIn1CanvasFader : MonoBehaviour
    {
        [SerializeField] private KeyCode fadeToggleKey = KeyCode.U;
        [SerializeField] private float tweenSpeed = 1f;
        [SerializeField] private AllIn1DemoScaleTween hideUiButtonTween;
        
        private bool isTweening = false;
        private float currentAlpha = 1f;
        private float targetAlpha = 1f;
        private CanvasGroup canvasGroup;
        private bool hideUiButtonTweenNotNull;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            hideUiButtonTweenNotNull = hideUiButtonTween != null;
        }

        private void Update()
        {
            if(Input.GetKeyDown(fadeToggleKey)) HideUiButtonPressed();
            
            if(!isTweening) return;
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.unscaledDeltaTime * tweenSpeed);
            canvasGroup.alpha = currentAlpha;
            if(targetAlpha == currentAlpha) isTweening = false;
        }

        public void HideUiButtonPressed()
        {
            if(currentAlpha < 0.01f) MakeCanvasVisibleTween();
            else MakeCanvasInvisibleTween();
            if(hideUiButtonTweenNotNull) hideUiButtonTween.ScaleUpTween();
        }

        private void MakeCanvasVisibleTween()
        {
            isTweening = true;
            targetAlpha = 1f;
        }

        private void MakeCanvasInvisibleTween()
        {
            isTweening = true;
            targetAlpha = 0f;
        }
    }
}
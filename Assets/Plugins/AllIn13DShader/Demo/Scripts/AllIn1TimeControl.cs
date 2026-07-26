using UnityEngine;
using UnityEngine.UI;

namespace AllIn13DShader
{
    public class AllIn1TimeControl : MonoBehaviour
    {
        [SerializeField] private KeyCode increaseTimeScale = KeyCode.UpArrow;
        [SerializeField] private KeyCode increaseTimeScaleAlt = KeyCode.W;
        [SerializeField] private KeyCode decreaseTimeScale = KeyCode.DownArrow;
        [SerializeField] private KeyCode decreaseTimeScaleAlt = KeyCode.S;
        [SerializeField] private KeyCode pauseKey = KeyCode.P;
        [SerializeField, Range(0f, 1f)] private float timeScaleInterval = 0.05f;
        [SerializeField] private Slider timeScaleSlider;
        [SerializeField] private Image pausedButtonImage;
        [SerializeField] private Color pausedButtonColor;
        private bool timeScaleSliderNotNull;
        private float lastChangeTime;
        private AllIn1DemoScaleTween pausedButtonTween;
        private Text pausedButtonText;

        private void Start()
        {
            timeScaleSliderNotNull = timeScaleSlider != null;
            pausedButtonTween = pausedButtonImage.GetComponent<AllIn1DemoScaleTween>();
            pausedButtonText = pausedButtonImage.GetComponentInChildren<Text>();
            UpdateTimeScaleUI();
            if(timeScaleSliderNotNull) timeScaleSlider.onValueChanged.AddListener(delegate { ChangeTimeScale(timeScaleSlider.value - Time.timeScale); });
        }

        private void Update()
        {
            if(Input.GetKeyDown(increaseTimeScale) || Input.GetKeyDown(increaseTimeScaleAlt)) ChangeTimeScale(timeScaleInterval);
            if(Input.GetKeyDown(decreaseTimeScale) || Input.GetKeyDown(decreaseTimeScaleAlt)) ChangeTimeScale(-timeScaleInterval);
            if(Input.GetKeyDown(pauseKey))
            {
                if(Time.timeScale < 0.01f) ChangeTimeScale(1f - Time.timeScale);
                else ChangeTimeScale(-Time.timeScale);
                pausedButtonTween.ScaleUpTween();
            }

            float timeScaleChangeInterval = 0.1f;
            if(!(Time.unscaledTime - lastChangeTime > timeScaleChangeInterval)) return;
            if(Input.GetKey(increaseTimeScale) || Input.GetKey(increaseTimeScaleAlt)) ChangeTimeScale(timeScaleInterval);
            if(Input.GetKey(decreaseTimeScale) || Input.GetKey(decreaseTimeScaleAlt)) ChangeTimeScale(-timeScaleInterval);
        }

        private void ChangeTimeScale(float changeAmount)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + changeAmount, 0.0f, 1f);
            lastChangeTime = Time.unscaledTime;
            UpdateTimeScaleUI();
        }

        private void UpdateTimeScaleUI()
        {
            if(timeScaleSliderNotNull) timeScaleSlider.value = Time.timeScale;
            if(Time.timeScale < 0.01f)
            {
                pausedButtonText.text = "Unpause";
                pausedButtonImage.color = pausedButtonColor;
            }
            else
            {
                pausedButtonText.text = "Pause";
                pausedButtonImage.color = Color.white;
            }
        }

        public void PauseUiButton()
        {
            if(Time.timeScale < 0.01f) Time.timeScale = 1f;
            else Time.timeScale = 0f;

            UpdateTimeScaleUI();
        }

        public void CurrentEffectChanged()
        {
            if(Time.timeScale < 0.01f)
            {
                Time.timeScale = 0.1f;
                UpdateTimeScaleUI();
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar { 
    public class TestBarControl : MonoBehaviour {

        [SerializeField] Transform barParent;
        [SerializeField] Transform sizeButtonParent;
        [SerializeField] TestSwitchBar[] barSelectors;
        ProgressBarPro[] bars;
        Button[] buttons;
        Slider slider;

	    void Start () {
            if (barParent != null)
			    bars = barParent.GetComponentsInChildren<ProgressBarPro>(true);

            if (sizeButtonParent != null) { 
                buttons = sizeButtonParent.GetComponentsInChildren<Button>();                  
                slider = GetComponentInChildren<Slider>();
                SetupButtons();
            }
	    }

        void SetupButtons() {
            Text text;
            Button button;

            for (int i = 0; i < buttons.Length; i++) {
                float currentValue = i / (float)(buttons.Length - 1);

                button = buttons[i];
                button.name = "Button_" + currentValue;
                text = button.GetComponentInChildren<Text>();
                text.text = currentValue.ToString();
                button.onClick.AddListener(delegate { SetSlider(currentValue); });
            }
        }

        void SetSlider(float value) {
            // This automatically controls the value of all bars
            if (slider != null)
                slider.value = value;
        }

        public void SetBars(float value) {
            if (bars != null)
                for (int i = 0; i < bars.Length; i++) 
                    bars[i].SetValue(value);

            if (barSelectors != null)
                for (int i = 0; i < barSelectors.Length; i++)
                    barSelectors[i].SetValue(value);            
        }

        public void SetRandomColor() {
            SetColor(new Color(Random.value, Random.value, Random.value));
        }

        public void SetColor(Color color) {
            if (bars != null)
                for (int i = 0; i < bars.Length; i++)
                    bars[i].SetBarColor(color);

            if (barSelectors != null)
                for (int i = 0; i < barSelectors.Length; i++)
                    barSelectors[i].SetBarColor(color);
        }
    }

}
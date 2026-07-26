using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AllIn13DShader
{
    [RequireComponent(typeof(InputHandler))]
    public class AllIn1DemoButtonPress : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private AllIn1DemoScaleTween scaleTween;
        [SerializeField] private Button button;

        [Space, Header("Label Settings")]
        [SerializeField] private bool completelyIgnoreLabel;
        [SerializeField] private bool showKeyLabel = true;
        [SerializeField] private TextMeshProUGUI keyLabel;
        
        private Coroutine resetButtonColorCr;

        private void Start()
        {
            if(!completelyIgnoreLabel)
            {
                if(!showKeyLabel)
                {
                    keyLabel.enabled = false;
                    enabled = false;
                }
                else
                {
                    keyLabel.text = $"(Key {inputHandler.GetTargetKey().ToString()})";
                }
            }
        }

        private void Update()
        {
            if(inputHandler.IsKeyPressed())
            {
                SimulateClick();
            }
        }

        private void SimulateClick()
        {
            if(button != null)
            {
                ColorBlock colors = button.colors;
                button.targetGraphic.color = colors.pressedColor;
                button.onClick.Invoke();
        
                if(resetButtonColorCr != null) StopCoroutine(resetButtonColorCr);
                resetButtonColorCr = StartCoroutine(ResetButtonColor());
            }
        }

        private IEnumerator ResetButtonColor()
        {
            yield return new WaitForSeconds(0.1f);
            button.targetGraphic.color = button.colors.normalColor;
        }
    }
}
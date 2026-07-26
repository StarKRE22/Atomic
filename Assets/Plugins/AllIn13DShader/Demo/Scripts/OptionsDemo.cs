using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn13DShader
{
    public class OptionsDemo : MonoBehaviour
    {
        [SerializeField] private Color lockedColor = Color.red;

        [Space, Header("Lock Camera")]
        [SerializeField] private AllIn1MouseTransformRotator allIn1MouseRotate;
        [SerializeField] private TextMeshProUGUI lockCamText;
        [SerializeField] private Image lockCamButtonImage;
        
        [Space, Header("Lock Cursor")]
        [SerializeField] private bool lockCursor = true;
        [SerializeField] private TextMeshProUGUI lockCursorText;
        [SerializeField] private Image lockCursorButtonImage;
        
        [Space, Header("Hide Ui")]
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private float uiCanvasSmoothing = 5f;

		[Space, Header("Demo Info")]
		[SerializeField] private bool showingDemoInfo = false;
		[SerializeField] private Image showDemoInfoButtonImage;

		private bool cursorIsLocked, camIsLocked, uiIsHidden;
        private float uiCanvasAlpha;

        private void Start()
        {
            cursorIsLocked = lockCursor;
            SetCursorLock();
            
            camIsLocked = false;
            SetCamLock();
        }

        private void Update()
        {
            uiCanvasGroup.alpha = Mathf.Lerp(uiCanvasGroup.alpha, uiIsHidden ? 0f : 1f, Time.unscaledDeltaTime * uiCanvasSmoothing);
        }
        
        public void ToggleCursorButtonPress()
        {
            cursorIsLocked = !cursorIsLocked;
            SetCursorLock();
		}

		public void ToggleShowInfo()
		{
			showingDemoInfo = !showingDemoInfo;
			showDemoInfoButtonImage.color = showingDemoInfo ? lockedColor : Color.clear;
		}

        private void SetCursorLock()
        {
            if(cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                lockCursorText.text = "Unlock Cursor";
                lockCursorButtonImage.color = lockedColor;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                lockCursorText.text = "Lock Cursor";
                lockCursorButtonImage.color = Color.clear;
            }
        }
        
        public void CamLockButtonPress()
        {
            camIsLocked = !camIsLocked;
            SetCamLock();
        }
        
        
        private void SetCamLock()
        {
            allIn1MouseRotate.enabled = !camIsLocked;
            if(camIsLocked)
            {
                lockCamText.text = "Unlock Camera";
                lockCamButtonImage.color = lockedColor;
            }
            else
            {
                lockCamText.text = "Lock Camera";
                lockCamButtonImage.color = Color.clear;
            }
        }
        
        public void HideUiButtonPress()
        {
            uiIsHidden = !uiIsHidden;
        }
    }
}
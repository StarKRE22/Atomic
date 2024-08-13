using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameExample.Engine
{
    public sealed class GameOverView : MonoBehaviour
    {
        public event UnityAction OnRestartClicked
        {
            add { this.restartButton.onClick.AddListener(value); }
            remove { this.restartButton.onClick.RemoveListener(value); }
        }

        [SerializeField]
        private TMP_Text messageText;
        
        [SerializeField]
        private Button restartButton;
        
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void SetMessageColor(Color color)
        {
            this.messageText.color = color;
        }

        public void SetMessage(string message)
        {
            this.messageText.text = message;
        }
    }
}
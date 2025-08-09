using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShooterGame.App
{
    public sealed class MainMenuView : MonoBehaviour
    {
        public event UnityAction OnStartClicked
        {
            add => _startButton.onClick.AddListener(value);
            remove => _startButton.onClick.RemoveListener(value);
        }

        public event UnityAction OnExitClicked
        {
            add => _exitButton.onClick.AddListener(value);
            remove => _exitButton.onClick.RemoveListener(value);
        }

        public event UnityAction<string> OnLevelChanged
        {
            add => _currentLevel.onValueChanged.AddListener(value);
            remove => _currentLevel.onValueChanged.RemoveListener(value);
        }

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private TMP_InputField _currentLevel;

        public void SetCurrentLevel(string level) => _currentLevel.text = level;

        public string GetCurrentLevel() => _currentLevel.text;
    }
}
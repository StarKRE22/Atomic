using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShooterGame.UI
{
    public sealed class StartScreenView : ScreenView
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

        public event UnityAction OnSelectLevelClicked
        {
            add => _selectLevelButton.onClick.AddListener(value);
            remove => _selectLevelButton.onClick.RemoveListener(value);
        }

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _selectLevelButton;

        [SerializeField]
        private Button _exitButton;
    }
}
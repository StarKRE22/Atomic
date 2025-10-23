using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShooterGame.UI
{
    public sealed class LevelItemView : MonoBehaviour
    {
        public event UnityAction OnClicked
        {
            add => _button.onClick.AddListener(value);
            remove => _button.onClick.RemoveListener(value);
        }

        [SerializeField]
        private TMP_Text _level;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _icon;
        
        public void SetLevel(string level)
        {
            _level.text = level;
        }
        
        public void SetAsCurrent()
        {
            _icon.color = Color.yellow;
            _button.interactable = true;
        }

        public void SetAsCompleted()
        {
            _icon.color = Color.green;
            _button.interactable = true;
        }

        public void SetAsNotCompleted()
        {
            _icon.color = Color.white;
            _button.interactable = false;
        }

    }
}
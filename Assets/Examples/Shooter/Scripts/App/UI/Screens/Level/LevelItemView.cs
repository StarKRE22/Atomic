using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShooterGame.App
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

        public void SetLevel(string level)
        {
            _level.text = level;
        }
    }
}
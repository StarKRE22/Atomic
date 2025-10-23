using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShooterGame.UI
{
    public sealed class LevelScreenView : ScreenView
    {
        public event UnityAction OnCloseClicked
        {
            add { _closeButton.onClick.AddListener(value); }
            remove { _closeButton.onClick.RemoveListener(value); }
        }

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private LevelItemView _itemPrefab;

        [SerializeField]
        private Transform _container;

        private readonly List<LevelItemView> _items = new();

        public LevelItemView CreateItem()
        {
            LevelItemView itemView = Instantiate(_itemPrefab, _container);
            _items.Add(itemView);
            return itemView;
        }

        public void ClearAllItems()
        {
            for (int i = 0, count = _items.Count; i < count; i++)
            {
                LevelItemView itemView = _items[i];
                Destroy(itemView.gameObject);
            }
            
            _items.Clear();
        }
    }
}
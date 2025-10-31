using Atomic.Elements;
using Atomic.Entities;
using TMPro;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class MoneyView : MonoBehaviour
    {
        [SerializeField]
        private SceneEntity _player;
        
        [SerializeField]
        private TMP_Text _moneyText;

        private Subscription<int> _subscription;

        private void Start()
        {
            _subscription = _player.GetMoney().Observe(this.OnMoneyChanged);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
        
        private void OnMoneyChanged(int money)
        {
            _moneyText.text = $"Money: {money}";
        }
    }
}
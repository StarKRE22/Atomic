using Atomic.Elements;
using Atomic.Entities;
using TMPro;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Displays the player's current money amount on the UI.
    /// </summary>
    /// <remarks>
    /// This component observes changes in the player's <see cref="IValue{T}"/> representing money
    /// and updates a <see cref="TMP_Text"/> element in real time.  
    /// It uses the Atomic reactive subscription system to automatically react to value updates.
    /// </remarks>
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
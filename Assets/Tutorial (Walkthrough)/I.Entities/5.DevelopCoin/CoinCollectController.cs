using Atomic.Entities;
using UnityEngine;

namespace Walkthrough
{
    public sealed class CoinCollectController : MonoBehaviour
    {
        [SerializeField]
        private int playerMoney;
        
        [Space]
        [SerializeField]
        private SceneEntity character;

        private void OnEnable()
        {
            this.character.GetTriggerEventReceiver().OnEntered += this.OnTriggerEntered;
        }

        private void OnDisable()
        {
            this.character.GetTriggerEventReceiver().OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            if (!collider.TryGetComponent(out IEntity entity))
            {
                return;
            }

            if (!entity.HasCoinTag())
            {
                return;
            }

            int coinMoney = entity.GetMoney().Value;
            this.playerMoney += coinMoney;
            Debug.Log($"Collected money: {coinMoney}");
            
            entity.GetGameObject().SetActive(false);
        }
    }
}
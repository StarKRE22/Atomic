using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Handles the logic for collecting coins when the entity enters a trigger area.
    /// </summary>
    /// <remarks>
    /// This behaviour listens for trigger enter events via <see cref="TriggerEvents"/> 
    /// and checks whether the collided entity has the <c>Coin</c> tag.  
    /// If so, it increases the entity’s money value and destroys the collected coin.
    /// </remarks>
    public sealed class CoinCollectBehaviour : IEntityInit, IEntityEnable, IEntityDisable
    {
        private TriggerEvents _triggerEvents;
        private IVariable<int> _money;

        public void Init(IEntity entity)
        {
            _money = entity.GetMoney();
            _triggerEvents = entity.GetTriggerEvents();
        }

        public void Enable(IEntity entity)
        {
            _triggerEvents.OnEntered += this.OnTriggerEntered;
        }

        public void Disable(IEntity entity)
        {
            _triggerEvents.OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            if (!collider.TryGetComponent(out IEntity target))
                return;
            
            if (!target.HasCoinTag())
                return;
            
            _money.Value++;
            SceneEntity.Destroy(target);
        }
    }
}
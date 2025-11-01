using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
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
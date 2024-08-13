using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class CoinCollectSystem : IContextInit, IContextEnable, IContextDisable
    {
        private IContext _playerContext;
        private IValue<IEntity> _character;

        public void Init(IContext context)
        {
            _playerContext = context;
            _character = context.GetCharacter();
        }

        public void Enable(IContext context)
        {
            _character.Value.GetTriggerEventReceiver().OnEntered += this.OnTriggerEntered;
        }

        public void Disable(IContext context)
        {
            _character.Value.GetTriggerEventReceiver().OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out IEntity target))
            {
                _playerContext.CollectCoin(target);
            }
        }
    }
}
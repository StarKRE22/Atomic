using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class FireAnimBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private static readonly int Fire = Animator.StringToHash(nameof(Fire));

        private ISignal _fireEvent;
        private Animator _animator;

        public void OnSpawn(IGameEntity entity)
        {
            _animator = entity.GetAnimator();
            _fireEvent = entity.GetFireEvent();
            _fireEvent.Subscribe(this.OnFire);
        }

        public void OnDespawn(IEntity entity)
        {
            _fireEvent.Unsubscribe(this.OnFire);
        }

        private void OnFire() => _animator.SetTrigger(Fire);
    }
}
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class FireAnimBehaviour : IEntityInit<IWorldEntity>, IEntityDispose
    {
        private static readonly int Fire = Animator.StringToHash(nameof(Fire));

        private ISignal _fireEvent;
        private Animator _animator;

        public void Init(IWorldEntity entity)
        {
            _animator = entity.GetAnimator();
            _fireEvent = entity.GetFireEvent();
            _fireEvent.Subscribe(this.OnFire);
        }

        public void Dispose(IEntity entity)
        {
            _fireEvent.Unsubscribe(this.OnFire);
        }

        private void OnFire() => _animator.SetTrigger(Fire);
    }
}
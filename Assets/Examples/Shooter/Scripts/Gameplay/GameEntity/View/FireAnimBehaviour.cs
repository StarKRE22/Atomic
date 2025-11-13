using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class FireAnimBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private static readonly int Fire = Animator.StringToHash(nameof(Fire));

        private ISignal _fireEvent;
        private Animator _animator;

        public void Init(IGameEntity entity)
        {
            _animator = entity.GetAnimator();
            _fireEvent = entity.GetFireEvent();
            _fireEvent.OnEvent += this.OnFire;
        }

        public void Dispose(IGameEntity entity)
        {
            _fireEvent.OnEvent -= this.OnFire;
        }

        private void OnFire() => _animator.SetTrigger(Fire);
    }
}
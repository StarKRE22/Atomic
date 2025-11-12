using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class TakeDamageAnimBehaviour : IGameEntityInit, IGameEntityDispose
    {
        private static readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));

        private ISignal<DamageArgs> _damageEvent;
        private Animator _animator;

        public void Init(IGameEntity entity)
        {
            _animator = entity.GetAnimator();
            _damageEvent = entity.GetTakeDamageEvent();
            _damageEvent.Subscribe(this.OnDamageTaken);
        }

        public void Dispose(IGameEntity entity)
        {
            _damageEvent.Unsubscribe(this.OnDamageTaken);
        }

        private void OnDamageTaken(DamageArgs _)
        {
            _animator.SetTrigger(TakeDamage);
        }
    }
}
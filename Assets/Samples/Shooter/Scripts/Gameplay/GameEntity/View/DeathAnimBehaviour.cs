using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable AsyncVoidMethod

namespace ShooterGame.Gameplay
{
    public sealed class DeathAnimBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        private static readonly int Death = Animator.StringToHash(nameof(Death));

        private readonly GameObject _view;
        private readonly float _deathDuration;

        private Health _health;
        private Animator _animator;
        
        public DeathAnimBehaviour(GameObject view, float deathDuration = 1.5f)
        {
            _deathDuration = deathDuration;
            _view = view;
        }

        private async void OnDeath()
        {
            _animator.SetTrigger(Death);
            await UniTask.WaitForSeconds(_deathDuration);
            _view.SetActive(false);
        }

        public void Init(IGameEntity entity)
        {
            _animator = entity.GetAnimator();
            _health = entity.GetHealth();
            _health.OnHealthEmpty += this.OnDeath;
        }

        public void Dispose(IEntity entity)
        {
            _health.OnHealthEmpty -= this.OnDeath;
        }
    }
}
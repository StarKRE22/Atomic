using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    public sealed class TakeDamageViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose<IGameEntity>
    {
        private readonly Transform _visual;
        private readonly Vector3 _originalScale;

        public TakeDamageViewBehaviour(Transform visual)
        {
            _visual = visual;
            _originalScale = visual.localScale;
        }

        public void Init(IGameEntity entity)
        {
            entity.GetTakeDamageEvent().Subscribe(this.OnTakeDamage);
        }

        public void Dispose(IGameEntity entity)
        {
            entity.GetTakeDamageEvent().Unsubscribe(this.OnTakeDamage);
        }

        private void OnTakeDamage(int _)
        {
            _visual.DOKill();

            _visual.localScale = _originalScale;
            _visual
                .DOScale(_originalScale * 1.1f, 0.1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => _visual.DOScale(_originalScale, 0.2f).SetEase(Ease.OutBounce));
        }
    }
}
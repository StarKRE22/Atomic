using System;
using Atomic.Elements;
using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TakeDamageViewBehaviour : IEntityInit<IUnit>, IEntityDispose<IUnit>
    {
        [SerializeField]
        private Transform _visual;

        [SerializeField]
        private Vector3 _originalScale = Vector3.one;

        public void Init(IUnit entity)
        {
            entity.GetTakeDamageEvent().Subscribe(this.OnTakeDamage);
        }

        public void Dispose(IUnit entity)
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
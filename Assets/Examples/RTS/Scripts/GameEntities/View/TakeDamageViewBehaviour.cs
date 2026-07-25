using System;
using Atomic.Elements;
using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TakeDamageViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose<IGameEntity>
    {
        [SerializeField]
        private Transform _visual;

        [SerializeField]
        private Vector3 _originalScale = Vector3.one;

        private ISignal<int> _takeDamageEvent;

        public void Init(IGameEntity entity)
        {
            _takeDamageEvent = entity.GetTakeDamageEvent(); 
            _takeDamageEvent.OnEvent += this.OnTakeDamage;
        }

        public void Dispose(IGameEntity entity)
        {
            _takeDamageEvent.OnEvent -= this.OnTakeDamage;
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
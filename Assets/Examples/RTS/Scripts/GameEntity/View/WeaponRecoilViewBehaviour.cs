using System;
using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class WeaponRecoilViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose<IGameEntity>
    {
        [SerializeField]
        private Transform _weapon;
      
        [SerializeField]
        private Vector3 _originalLocalPos = Vector3.zero;

        public void Init(IGameEntity entity)
        {
            entity.GetFireEvent().Subscribe(this.OnFire);
        }

        public void Dispose(IGameEntity entity)
        {
            entity.GetFireEvent().Unsubscribe(this.OnFire);
        }

        private void OnFire(IGameEntity _)
        {
            _weapon.DOKill();

            const float recoilDistance = -0.3f;
            const float recoilTime = 0.05f;
            const float returnTime = 0.1f;

            Sequence seq = DOTween.Sequence();
            seq.Append(_weapon.DOLocalMoveZ(_originalLocalPos.z + recoilDistance, recoilTime).SetEase(Ease.OutQuad));
            seq.Append(_weapon.DOLocalMoveZ(_originalLocalPos.z, returnTime).SetEase(Ease.OutBack));
        }
    }
}
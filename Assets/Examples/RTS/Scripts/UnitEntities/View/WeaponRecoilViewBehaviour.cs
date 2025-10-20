using System;
using Atomic.Elements;
using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class WeaponRecoilViewBehaviour : IEntityInit<IUnitEntity>, IEntityDispose<IUnitEntity>
    {
        [SerializeField]
        private Transform _weapon;
      
        [SerializeField]
        private Vector3 _originalLocalPos = Vector3.zero;

        public void Init(IUnitEntity entity)
        {
            entity.GetFireEvent().Subscribe(this.OnFire);
        }

        public void Dispose(IUnitEntity entity)
        {
            entity.GetFireEvent().Unsubscribe(this.OnFire);
        }

        private void OnFire(IUnitEntity _)
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
using Atomic.Entities;
using DG.Tweening;
using UnityEngine;

namespace RTSGame
{
    public sealed class WeaponRecoilViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose<IGameEntity>
    {
        private readonly Transform _weapon;
        private readonly Vector3 _originalLocalPos;

        public WeaponRecoilViewBehaviour(Transform weapon)
        {
            _weapon = weapon;
            _originalLocalPos = weapon.localPosition;
        }

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
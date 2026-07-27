using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public static class RotateUseCase
    {
        public static async UniTask RotateAt(this GameEntityView entity, Vector3 position)
        {
            Transform transform = entity.Entity.GetTransform();

            transform.DOKill();
            Vector3 direction = position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            
            await transform
                .DORotateQuaternion(rotation, .15f)
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();
        }
    }
}
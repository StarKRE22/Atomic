using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public static class MoveUseCase
    {
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));

        public static void SetAnimatorMoving(this GameEntityView entity, bool isMoving)
        {
            Animator animator = entity.GetValue(GameEntityViewAPI.Animator);
            animator.SetBool(IsMoving, isMoving);
        }

        public static async UniTask MoveAt(this GameEntityView entity, Vector3 position)
        {
            Transform transform = entity.GetValue(GameEntityViewAPI.Transform);
            transform.DOKill();
            await transform
                .DOMove(position, .5f)
                .SetEase(Ease.InOutSine)
                .AsyncWaitForCompletion();
        }
    }
}
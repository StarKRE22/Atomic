using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public static class TakeDamageUseCase
    {
        private static readonly int Hit = Animator.StringToHash(nameof(Hit));
        
        public static async UniTask TakeDamage(this GameEntityView entityView, TakeDamageEventArgs args)
        {
            entityView.AnimatorHit();
            entityView.UpdateHealthBar(args.health);
            entityView.HighlightHit();

            Transform transform = entityView.Entity.GetTransform();
            transform.DOKill();
            transform.localScale = Vector3.one;
            await transform
                .DOPunchScale(Vector3.one * 0.1f, 0.25f)
                .AsyncWaitForCompletion();
        }

        public static void HighlightHit(this GameEntityView entityView)
        {
            CharacterRenderer characterView = entityView.Entity.GetCharacterRenderer();
            characterView.PlayHit();
        }

        public static void AnimatorHit(this GameEntityView entityView)
        {
            Animator animator = entityView.Entity.GetAnimator();
            animator.SetTrigger(Hit);
        }
    }
}
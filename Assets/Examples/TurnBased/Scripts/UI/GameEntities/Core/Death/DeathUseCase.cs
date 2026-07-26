using System;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public static class DeathUseCase
    {
        private static readonly int Death = Animator.StringToHash(nameof(Death));

        public static async UniTask Die(this GameEntityView entity, IUIContext ui)
        {
            entity.UpdateHealthBar(0);

            Animator animator = entity.GetValue(GameEntityViewAPI.Animator);
            animator.SetTrigger(Death);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            GameObjectPool prefabPool = ui.GetValue(UIContextAPI.GameObjectPrefabPool);
            GameObject deathEffect = ui.GetValue(UIContextAPI.DeathEffect);

            Transform transform = entity.GetValue(GameEntityViewAPI.Transform);
            Vector3 effectPosition = transform.position;
            effectPosition.y = 1.5f;
            prefabPool.Rent(deathEffect, effectPosition, deathEffect.transform.rotation);
            
            transform
                .DOScale(Vector3.zero, 0.5f)
                .OnComplete(() => entity.Despawn(ui))
                .SetTarget(transform.gameObject);
        }
        
        public static async UniTask DieFromBounds(this GameEntityView view, Vector3 from, Vector3 to, IUIContext ui)
        {
            view.UpdateHealthBar(0);
            view.AnimatorHit();
            view.HighlightHit();

            GameObjectPool prefabPool = ui.GetValue(UIContextAPI.GameObjectPrefabPool);
            GameObject waterSplashEffect = ui.GetValue(UIContextAPI.WaterSplashEffect);

            var waterSplashPosition = to;
            waterSplashPosition.y = 0f;

            Transform target = view.GetValue(GameEntityViewAPI.Transform);
            target.DOKill();
            target.position = from;

            DOVirtual.DelayedCall(0.15f,
                () => prefabPool.Rent(waterSplashEffect, waterSplashPosition, Quaternion.identity)
            );

            await DOTween.Sequence()
                .Append(target.DOJump(to, 1f, 1, 0.25f))
                .Append(target.DOScale(Vector3.zero, 0.25f))
                .AppendCallback(() => view.Despawn(ui))
                .AsyncWaitForCompletion();
        }
    }
}
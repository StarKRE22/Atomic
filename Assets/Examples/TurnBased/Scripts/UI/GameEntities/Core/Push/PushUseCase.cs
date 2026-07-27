using Atomic.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public static class PushUseCase
    {
        public static async UniTask PushTarget(this IUIContext ui, PushTargetEventArgs args)
        {
            GameEntityView source = ui.GetEntityView(args.source);
            GameEntityView target = ui.GetEntityView(args.target);
            Vector3 from = ui.GetWorldPosition(args.sourcePosition);
            Vector3 to = ui.GetWorldPosition(args.targetPosition);


            Transform sourceTransform = source.Entity.GetTransform();
            target.RotateAt(sourceTransform.position).Forget();

            // source.AnimatorHit();
            // source.UpdateHealthBar();
            // source.HighlightHit();


            await DOTween.Sequence()
                .Append(
                    sourceTransform
                        .DOMove(Vector3.Lerp(from, to, 0.5f), 0.15f)
                        .SetEase(Ease.OutCirc)
                        .ChangeStartValue(from)
                )
                .AsyncWaitForCompletion();

            sourceTransform
                .DOPunchScale(Vector3.one * 0.1f, 0.25f);
            sourceTransform
                .DOMove(from, 0.25f)
                .SetEase(Ease.OutCirc);
        }
    }
}
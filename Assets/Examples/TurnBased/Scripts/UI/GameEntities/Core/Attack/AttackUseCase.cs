using System;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public static class AttackUseCase
    {
        private static readonly int Attack = Animator.StringToHash(nameof(Attack));

        private static void AttackAnimator(this GameEntityView source)
        {
            Animator animator = source.GetValue(GameEntityViewAPI.Animator);
            animator.SetTrigger(Attack);
        }

        public static async UniTask StartAttack(this GameEntityView attacker, GameEntityView target)
        {
            Vector3 attackerPosition = attacker.GetValue(GameEntityViewAPI.Transform).position;
            Vector3 targetPosition = target.GetValue(GameEntityViewAPI.Transform).position;
            Vector3 movePosition = Vector3.Lerp(attackerPosition, targetPosition, 0.4f);

            target.RotateAt(attackerPosition).Forget();
            
            await attacker.RotateAt(movePosition);

            attacker.AnimatorJump();
            await attacker.MoveAt(movePosition);
            
            attacker.AttackAnimator();
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        }

        public static async UniTask EndAttack(this GameEntityView source, Vector3 sourcePosition)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.25f));
            source.AnimatorJump();
            await UniTask.Delay(TimeSpan.FromSeconds(.1f));
            await source.MoveAt(sourcePosition);
        }
    }
}
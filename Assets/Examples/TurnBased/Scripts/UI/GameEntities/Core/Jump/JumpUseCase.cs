using Atomic.Entities;
using UnityEngine;

namespace Game.UI
{
    public static class JumpUseCase
    {
        private static readonly int Jump = Animator.StringToHash(nameof(Jump));

        public static void AnimatorJump(this GameEntityView source)
        {
            Animator animator = source.GetValue(GameEntityViewAPI.Animator);
            animator.SetTrigger(Jump);
        }
    }
}
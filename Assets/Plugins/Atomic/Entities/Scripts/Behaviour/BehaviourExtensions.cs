using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static class BehaviourExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenInit(this IEntity entity, Action action)
        {
            entity.OnInitialized += action;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenEnable(this IEntity entity, Action action)
        {
            entity.OnEnabled += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenDisable(this IEntity entity, Action action)
        {
            entity.OnDisabled += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenDispose(this IEntity entity, Action action)
        {
            entity.OnDisposed += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnUpdated += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenFixedUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnFixedUpdated += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenLateUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnLateUpdated += action;
        }
    }
}
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static class BehaviourExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action WhenInit(this IEntity entity, in Action action)
        {
            entity.OnInitialized += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action WhenEnable(this IEntity entity, in Action action)
        {
            entity.OnEnabled += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action WhenDisable(this IEntity entity, in Action action)
        {
            entity.OnDisabled += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action WhenDispose(this IEntity entity, in Action action)
        {
            entity.OnDisposed += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<float> WhenUpdate(this IEntity entity, in Action<float> action)
        {
            entity.OnUpdated += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<float> WhenFixedUpdate(this IEntity entity, in Action<float> action)
        {
            entity.OnFixedUpdated += action;
            return action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<float> WhenLateUpdate(this IEntity entity, in Action<float> action)
        {
            entity.OnLateUpdated += action;
            return action;
        }
    }
}
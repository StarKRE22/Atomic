using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpawnSubscription WhenSpawn(this ISpawnable source, Action action)
        {
            source.OnSpawned += action;
            return new SpawnSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DespawnSubscription WhenDespawn(this ISpawnable source, Action action)
        {
            source.OnDespawned += action;
            return new DespawnSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnableSubscription WhenEnable(this IActivatable source, Action action)
        {
            source.OnEnabled += action;
            return new EnableSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisableSubscription WhenDisable(this IActivatable source, Action action)
        {
            source.OnDisabled += action;
            return new DisableSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UpdateSubscription WhenUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnUpdated += action;
            return new UpdateSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedUpdateSubscription WhenFixedUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnFixedUpdated += action;
            return new FixedUpdateSubscription(source, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateUpdateSubscription WhenLateUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnLateUpdated += action;
            return new LateUpdateSubscription(source, action);
        }
    }
}
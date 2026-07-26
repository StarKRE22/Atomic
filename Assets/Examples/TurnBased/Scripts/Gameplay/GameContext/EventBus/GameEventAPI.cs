using Atomic.Events;
using Atomic.Events.Atomic.Events;
using UnityEngine;

namespace Game.Gameplay
{
    public static class GameEventAPI
    {
        // Game System
        public static readonly EventKey<IGameEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
        public static readonly EventKey<IGameEventBus> PlayerTurnEnded = new(nameof(PlayerTurnEnded));

        public static readonly EventKey<IGameEventBus> EnemyTurnEnded = new(nameof(EnemyTurnEnded));

        // Game Entities
        public static readonly EventKey<IGameEventBus, SpawnEventArgs> EntitySpawned = new(nameof(EntitySpawned));
        public static readonly EventKey<IGameEventBus, IGameEntity> EntityDespawned = new(nameof(EntityDespawned));
        public static readonly EventKey<IGameEventBus, TakeDamageEventArgs> EntityDamaged = new(nameof(EntityDamaged));
        public static readonly EventKey<IGameEventBus, AttackEventArgs> EntityAttackStarted = new(nameof(EntityAttackStarted));
        public static readonly EventKey<IGameEventBus, AttackEventArgs> EntityAttackEnded = new(nameof(EntityAttackEnded));
        public static readonly EventKey<IGameEventBus, MoveEventArgs> EntityMoved = new(nameof(EntityMoved));
        public static readonly EventKey<IGameEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
        public static readonly EventKey<IGameEventBus, PushOutEventArgs> EntityPushedOut = new(nameof(EntityPushedOut));
        public static readonly EventKey<IGameEventBus, PushTargetEventArgs> EntityPushedTarget = new(nameof(EntityPushedTarget));
        public static readonly EventKey<IGameEventBus, PushedEventArgs> EntityPushed = new(nameof(EntityPushed));
    }
}
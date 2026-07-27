using Atomic.Elements;
using Atomic.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace BeginnerGame
{
    [GenerateEntityExtensionsAPI]
    public static partial class EntityAPI
    {
        public static readonly TagKey Character = new(nameof(Character));
        public static readonly TagKey Coin = new(nameof(Coin));
        public static readonly TagKey GameContext = new(nameof(GameContext));

        public static readonly ValueKey<Transform> Transform = new(nameof(Transform));
        public static readonly ValueKey<IVariable<Vector3>> MovementDirection = new(nameof(MovementDirection));
        public static readonly ValueKey<IValue<float>> MovementSpeed = new(nameof(MovementSpeed));
        public static readonly ValueKey<IReactiveVariable<int>> Money = new(nameof(Money));
        public static readonly ValueKey<SpawnInfo> CoinSpawnInfo = new(nameof(CoinSpawnInfo));
        public static readonly ValueKey<InputMap> InputMap = new(nameof(InputMap));
        public static readonly ValueKey<TriggerEvents> TriggerEvents = new(nameof(TriggerEvents));
        public static readonly ValueKey<IDictionary<TeamType, IEntity>> Players = new(nameof(Players));
        public static readonly ValueKey<ICooldown> GameCountdown = new(nameof(GameCountdown));
    }
}

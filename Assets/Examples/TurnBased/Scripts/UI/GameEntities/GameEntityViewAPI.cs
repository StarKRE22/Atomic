using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    [EntityAPI]
    public static partial class GameEntityViewAPI
    {
        public static readonly ValueKey<IGameEntity, Transform> Transform = new(nameof(Transform));
        public static readonly ValueKey<IGameEntity, Animator> Animator = new(nameof(Animator));
        public static readonly ValueKey<IGameEntity, ProgressBarPro> HealthBar = new(nameof(HealthBar));
        public static readonly ValueKey<IGameEntity, CharacterRenderer> CharacterRenderer = new(nameof(CharacterRenderer));
    }
}

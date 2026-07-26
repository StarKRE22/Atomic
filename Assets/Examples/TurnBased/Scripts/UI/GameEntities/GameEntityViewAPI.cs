using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public static class GameEntityViewAPI
    {
        public static ValueKey<IGameEntity, Transform> Transform = new(nameof(Transform));
        public static ValueKey<IGameEntity, Animator> Animator = new(nameof(Animator));
        public static ValueKey<IGameEntity, ProgressBarPro> HealthBar = new(nameof(HealthBar));
        public static ValueKey<IGameEntity, CharacterRenderer> CharacterRenderer = new(nameof(CharacterRenderer));
    }
}
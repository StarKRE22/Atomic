using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.UI
{
    [EntityAPI]
    public static partial class GameUIAPI
    {
        public static readonly ValueKey<IGameUI, Transform> PopupTransform = new(nameof(PopupTransform));
        public static readonly ValueKey<IGameUI, GameOverView> GameOverViewPrefab = new(nameof(GameOverViewPrefab));
        public static readonly ValueKey<IGameUI, GameOverView> GameOverView = new(nameof(GameOverView));
    }
}

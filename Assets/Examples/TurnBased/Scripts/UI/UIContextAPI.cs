using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [EntityAPI]
    public static partial class UIContextAPI
    {
        public static readonly ValueKey<Camera> Camera = new(nameof(Camera));
        public static readonly ValueKey<UICommandQueue> CommandQueue = new(nameof(CommandQueue));
        public static readonly ValueKey<GameEntityCollectionView> EntityCollectionView =
            new(nameof(GameEntityCollectionView));
        public static readonly ValueKey<GameBoardView> GameBoardView = new(nameof(GameBoardView));
        public static readonly ValueKey<IReactiveVariable<IGameEntity>> SelectedCharacter =
            new(nameof(SelectedCharacter));
        public static readonly ValueKey<IFunction<bool>> InputCondition = new(nameof(InputCondition));
        public static readonly ValueKey<GameObjectPool> GameObjectPrefabPool = new(nameof(GameObjectPrefabPool));
        public static readonly ValueKey<TurnView> TurnView = new(nameof(TurnView));
        public static readonly ValueKey<Button> EndTurnButton = new(nameof(EndTurnButton));
        public static readonly ValueKey<GameObject> WaterSplashEffect = new(nameof(WaterSplashEffect));
        public static readonly ValueKey<GameObject> DeathEffect = new(nameof(DeathEffect));
    }
}

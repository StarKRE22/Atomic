using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "GameContext",
        menuName = "Game/Gameplay/GameContext"
    )]
    public sealed class GameContextFactory : ScriptableEntityFactory<IGameContext, NoArgs>
    {
        [SerializeField]
        private Const<int> _spawnDamage = 1;

        [SerializeField]
        private EntitySpawnInfo[] _waves;

        [SerializeField]
        private EntitySystemInstaller _entitySystemInstaller;

        [SerializeField]
        private GameBoardInstaller _gameBoardInstaller;

        protected override IGameContext Create(int tagCapacity, int valueCapacity, int behaviourCapacity,
            Entity.Settings settings, NoArgs args)
        {
            GameContext context = new GameContext(this.name, tagCapacity, valueCapacity, behaviourCapacity, settings);

            GameEventBus eventBus = new GameEventBus();
            context.AddValue(GameContextAPI.EventBus, eventBus);
            context.AddValue(GameContextAPI.CurrentTurn, new ThreadSafeReactiveVariable<int>());
            context.AddValue(GameContextAPI.EnemyWaves, new List<EntitySpawnInfo>(_waves));
            context.AddValue(GameContextAPI.GameState, new ThreadSafeReactiveVariable<GameState>(GameState.Playing));
            context.AddValue(GameContextAPI.IsWin, new ThreadSafeReactiveVariable<bool>());
            context.AddValue(GameContextAPI.SpawnDamage, _spawnDamage);

            // context.WhenTick(_ => eventBus.Flush());
            
            _entitySystemInstaller.Install(context);
            _gameBoardInstaller.Install(context);

            return context;
        }
    }
}
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameContextFactory",
        menuName = "RTSGame/New GameContextFactory"
    )]
    public sealed class GameContextFactory : ScriptableEntityFactory<GameContext>
    {
        [SerializeField]
        private UnitsSystemInstaller _gameEntityInstaller;

        [SerializeField]
        private PlayerSystemInstaller _playerSystemInstaller;

        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        public override GameContext Create()
        {
            var context = new GameContext(
                this.name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            _gameEntityInstaller.Install(context);
            _playerSystemInstaller.Install(context);
            context.AddTeamViewConfig(_teamViewConfig);
            context.AddSpatialHash(new SpatialHash<IUnit>(10, unit => unit.GetPosition().Value,256, 64));
            return context;
        }
    }
}
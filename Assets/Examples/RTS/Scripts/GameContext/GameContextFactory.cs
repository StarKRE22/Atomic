using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameContextFactory",
        menuName = "SampleGame/GameContext/New GameContextFactory"
    )]
    public sealed class GameContextFactory : ScriptableEntityFactory<IGameContext>
    {
        [SerializeField]
        private GameEntitySystemInstaller _gameEntityInstaller;

        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        public override IGameContext Create()
        {
            var context = new GameContext();
            _gameEntityInstaller.Install(context);
            context.AddTeamViewConfig(_teamViewConfig);
            return context;
        }
    }
}
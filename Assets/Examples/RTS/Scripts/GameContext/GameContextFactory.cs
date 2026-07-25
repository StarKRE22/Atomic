using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameContextFactory",
        menuName = "RTSGame/New GameContextFactory"
    )]
    public sealed class GameContextFactory : ScriptableEntityFactory<IGameContext, NoArgs>
    {
        [SerializeField]
        private EntitySystemInstaller _gameEntityInstaller;

        [SerializeField]
        private PlayerSystemInstaller _playerSystemInstaller;
        
        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        protected override IGameContext Create(int tagCapacity,
            int valueCapacity,
            int behaviourCapacity,
            Entity.Settings settings,
            NoArgs args)
        {
            GameContext context = new GameContext(
                this.name,
                tagCapacity,
                valueCapacity,
                behaviourCapacity
            );

            _gameEntityInstaller.Install(context);
            _playerSystemInstaller.Install(context);
            
            context.AddTeamViewConfig(_teamViewConfig);
            return context;
        }
    }
}
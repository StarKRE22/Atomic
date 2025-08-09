using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public interface IGameContext : IEntity
    {
    }

    public sealed class GameContext : SceneEntitySingleton<GameContext>, IGameContext
    {
        public TeamCatalog GetTeamConfig()
        {
            throw new System.NotImplementedException();
        }

        public void TryGetTeamCatalog(out TeamCatalog catalog)
        {
            throw new System.NotImplementedException();
        }
    }
}
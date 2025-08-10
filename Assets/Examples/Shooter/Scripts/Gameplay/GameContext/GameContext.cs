using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public interface IGameContext : IEntity
    {
        IEvent<KillArgs> GetKillEvent();
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

        public IEvent<KillArgs> GetKillEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
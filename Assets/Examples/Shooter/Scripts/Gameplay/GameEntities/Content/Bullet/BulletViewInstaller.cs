using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletViewInstaller : GameEntityInstaller
    {
        [SerializeField]
        private Renderer _renderer;

        public override void Install(IGameEntity entity)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            entity.AddRenderer(_renderer);
            entity.AddBehaviour(new TeamColorBehaviour(gameContext));
        }
    }
}
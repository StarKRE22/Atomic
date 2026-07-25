using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class GamePresenter : MonoBehaviour
    {
        [SerializeField]
        private GameEntityWorldView _worldView;

        [SerializeField]
        private GameEntityViewPool _viewPool;
        
        private void Start()
        {
            _ = _viewPool.InitAsync(GameEntityType.Projectile, 4096);
            IGameContext gameContext = FindAnyObjectByType<GameRunner>().GameContext;
            EntityWorld<IGameEntity> entities = gameContext.GetEntityWorld();
            _worldView.Activate(entities);
        }
    }
}
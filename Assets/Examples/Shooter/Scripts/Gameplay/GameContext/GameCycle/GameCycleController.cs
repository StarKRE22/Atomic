using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameCycleController : IEntitySpawn<IGameContext>, IEntityUpdate
    {
        private IReactiveVariable<float> _gameTime;

        public void OnSpawn(IGameContext context)
        {
            _gameTime = context.GetGameTime();
            Debug.Log("<color=yellow>Game Started!</color>");
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (_gameTime.Value <= 0)
                return;

            _gameTime.Value -= deltaTime;

            if (_gameTime.Value <= 0) 
                Debug.Log("<color=yellow>Game Finished</color>");
        }
    }
}
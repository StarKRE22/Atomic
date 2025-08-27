using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameCycleController : IEntitySpawn<IGameContext>, IEntityUpdate
    {
        private IVariable<float> _gameTime;
        private IEvent _gameOverEvent;

        public void OnSpawn(IGameContext context)
        {
            _gameTime = context.GetGameTime();
            _gameOverEvent = context.GetGameOverEvent();
            Debug.Log("<color=yellow>Game Started!</color>");
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (_gameTime.Value <= 0)
                return;

            _gameTime.Value -= deltaTime;

            if (_gameTime.Value <= 0)
            {
                _gameOverEvent.Invoke();
                Debug.Log("<color=yellow>Game Finished</color>");
            }
        }
    }
}
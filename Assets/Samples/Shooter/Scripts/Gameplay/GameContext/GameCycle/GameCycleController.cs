using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameCycleController : IEntityInit<IGameContext>, IEntityEnable, IEntityUpdate
    {
        private IVariable<float> _gameTime;
        private IEvent _gameOverEvent;

        public void Init(IGameContext context)
        {
            _gameTime = context.GetGameTime();
            _gameOverEvent = context.GetGameOverEvent();
        }

        public void Enable(IEntity entity)
        {
            Debug.Log("<color=yellow>Game Started!</color>");
        }

        public void Update(IEntity entity, float deltaTime)
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
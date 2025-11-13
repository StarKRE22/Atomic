using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameCycleController : IGameContextInit, IGameContextEnable, IGameContextTick
    {
        private IVariable<float> _gameTime;
        private IEvent _gameOverEvent;

        public void Init(IGameContext context)
        {
            _gameTime = context.GetGameTime();
            _gameOverEvent = context.GetGameOverEvent();
        }

        public void Enable(IGameContext context)
        {
            Debug.Log("<color=yellow>Game Started!</color>");
        }

        public void Tick(IGameContext context, float deltaTime)
        {
            if (_gameTime.Value <= 0)
                return;

            _gameTime.Value -= deltaTime;

            if (_gameTime.Value <= 0)
            {
                _gameOverEvent.Invoke();
                context.Disable();
                Debug.Log("<color=yellow>Game Finished</color>");
            }
        }
    }
}
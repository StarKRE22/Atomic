using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class QuitController : IAppContextInit, IAppContextTick
    {
        private IValue<KeyCode> _exitKey;

        public void Init(IAppContext context)
        {
            _exitKey = context.GetExitKeyCode();
        }

        public void Tick(IAppContext entity, float deltaTime)
        {
            if (Input.GetKey(_exitKey.Value) && MenuUseCase.InMenu()) 
                QuitUseCase.Quit();
        }
    }
}
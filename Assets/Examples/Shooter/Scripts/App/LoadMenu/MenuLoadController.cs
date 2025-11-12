using Atomic.Elements;
using ShooterGame.App;
using UnityEngine;

namespace ShooterGame
{
    public sealed class MenuLoadController : IAppContextInit, IAppContextTick
    {
        private IValue<KeyCode> _exitKey;
        
        public void Init(IAppContext context)
        {
            _exitKey = context.GetExitKeyCode();
        }

        public void Tick(IAppContext entity, float deltaTime)
        {
            if (Input.GetKeyDown(_exitKey.Value) && !MenuUseCase.InMenu()) 
                MenuUseCase.LoadMenu().Forget();
        }
    }
}
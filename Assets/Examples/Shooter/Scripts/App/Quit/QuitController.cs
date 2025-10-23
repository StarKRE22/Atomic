using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class QuitController : IEntityInit<IAppContext>, IEntityTick
    {
        private IValue<KeyCode> _exitKey;

        public void Init(IAppContext context)
        {
            _exitKey = context.GetExitKeyCode();
        }

        public void Tick(IEntity entity, float deltaTime)
        {
            if (Input.GetKey(_exitKey.Value) && MenuUseCase.InMenu()) 
                QuitUseCase.Quit();
        }
    }
}
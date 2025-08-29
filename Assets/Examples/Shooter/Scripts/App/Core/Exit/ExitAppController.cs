using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class ExitAppController : IEntityInit<IAppContext>, IEntityUpdate
    {
        private IValue<KeyCode> _exitKey;

        public void Init(IAppContext context)
        {
            _exitKey = context.GetExitKeyCode();
        }

        public void Update(IEntity entity, float deltaTime)
        {
            if (Input.GetKey(_exitKey.Value) && MenuUseCase.InMenu()) 
                ExitAppUseCase.Exit();
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class ExitAppController : IEntitySpawn<IAppContext>, IEntityUpdate
    {
        private IValue<KeyCode> _exitKey;

        public void OnSpawn(IAppContext context)
        {
            _exitKey = context.GetExitKeyCode();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (Input.GetKey(_exitKey.Value)) 
                ExitAppUseCase.Exit();
        }
    }
}
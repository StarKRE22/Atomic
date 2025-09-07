using Atomic.Entities;
using ShooterGame.App;
using UnityEngine;

namespace ShooterGame
{
    public sealed class ExitMenuController : IEntityUpdate
    {
        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !MenuUseCase.InMenu()) 
                MenuUseCase.LoadMenu().Forget();
        }
    }
}
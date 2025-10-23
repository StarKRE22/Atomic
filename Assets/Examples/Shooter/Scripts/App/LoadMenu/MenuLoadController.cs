using Atomic.Entities;
using ShooterGame.App;
using UnityEngine;

namespace ShooterGame
{
    public sealed class MenuLoadController : IEntityTick
    {
        public void Tick(IEntity entity, float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !MenuUseCase.InMenu()) 
                MenuUseCase.LoadMenu().Forget();
        }
    }
}
using ShooterGame.App;
using UnityEngine;

namespace ShooterGame
{
    public sealed class MenuLoadController : IAppContextTick
    {
        public void Tick(IAppContext entity, float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !MenuUseCase.InMenu()) 
                MenuUseCase.LoadMenu().Forget();
        }
    }
}
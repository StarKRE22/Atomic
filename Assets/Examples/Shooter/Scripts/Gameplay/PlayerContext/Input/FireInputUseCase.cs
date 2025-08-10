using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class FireInputUseCase
    {
        public static bool FireRequired(IPlayerContext playerContext, IGameContext gameContext) => 
            GameCycleUseCase.IsPlaying(gameContext) && Input.GetKeyDown(playerContext.GetInputMap().Fire);
    }
}
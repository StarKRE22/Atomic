// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class FireInputUseCase
//     {
//         public static bool FireRequired(in IPlayerContext playerContext, in IGameContext gameContext) => 
//             GameCycleUseCase.IsPlaying(gameContext) && Input.GetKeyDown(playerContext.GetInputMap().Fire);
//     }
// }
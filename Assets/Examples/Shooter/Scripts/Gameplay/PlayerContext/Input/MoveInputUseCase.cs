// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class MoveInputUseCase
//     {
//         public static Vector3 GetMoveDirection(in IPlayerContext playerContext, in IGameContext gameContext)
//         {
//             Vector3 direction = Vector3.zero;
//             if (!GameCycleUseCase.IsPlaying(gameContext))
//                 return direction;
//
//             InputMap inputMap = playerContext.GetInputMap();
//             if (Input.GetKey(inputMap.MoveForward))
//                 direction.z = 1;
//             else if (Input.GetKey(inputMap.MoveBack))
//                 direction.z = -1;
//
//             if (Input.GetKey(inputMap.MoveLeft))
//                 direction.x = -1;
//             else if (Input.GetKey(inputMap.MoveRight))
//                 direction.x = 1;
//
//             return direction;
//         }
//     }
// }
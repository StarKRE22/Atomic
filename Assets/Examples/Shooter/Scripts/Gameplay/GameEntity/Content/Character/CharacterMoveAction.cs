// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterMoveAction
//     {
//         entity.AddMoveAction(new BaseAction<MoveArgs>(args =>
//         {
//             if (entity.GetHealth().Value <= 0)
//                 return;
//
//             if (args.direction == Vector3.zero)
//                 return;
//
//             MoveUseCase.MoveStep(entity, args.direction, args.deltaTime);
//             RotateUseCase.RotateStep(entity, args.direction, args.deltaTime);
//             entity.GetMoveEvent().Invoke(args);
//         }));
//     }
// }
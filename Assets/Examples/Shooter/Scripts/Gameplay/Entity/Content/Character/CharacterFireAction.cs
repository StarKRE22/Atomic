// using Atomic.Elements;
// using Atomic.Entities;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterFireAction : IAction
//     {
//         private readonly IEntity _entity;
//
//         public CharacterFireAction(IEntity entity)
//         {
//             _entity = entity;
//         }
//
//         public void Invoke()
//         {
//             if (_entity.GetFireCondition().Value)
//             {
//                 _entity.GetWeapon().GetFireAction().Invoke();
//                 _entity.GetFireEvent().Invoke();
//             }
//         }
//     }
// }
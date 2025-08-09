// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class WeaponCoreInstaller : SceneEntityInstaller
//     {
//         [SerializeField]
//         private Transform _firePoint;
//
//         public override void Install(IEntity entity)
//         {
//             entity.AddFirePoint(_firePoint);
//             entity.AddFireEvent(new BaseEvent());
//             entity.AddFireAction(new BaseAction(() =>
//             {
//                 FireBulletUseCase.FireBullet(entity, GameContext.Instance);
//                 entity.GetFireEvent().Invoke();
//             }));
//             entity.AddTeam(new ReactiveVariable<TeamType>(TeamType.NEUTRAL));
//         }
//     }
// }
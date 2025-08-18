// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     [CreateAssetMenu(
//         fileName = "HeadquartersInstaller",
//         menuName = "SampleGame/Entities/New HeadquartersInstaller"
//     )]
//     public sealed class HeadquartersInstaller : ScriptableEntityFactory<>
//     {
//         [SerializeField]
//         private int _health;
//
//         public override void Install(IEntity entity)
//         {
//             this.InstallLife(entity);
//         }
//
//         private void InstallLife(IEntity entity)
//         {
//             entity.AddUnitTag();
//             entity.AddDamageableTag();
//             entity.AddHealth(new Health(_health, _health));
//             entity.AddBehaviour<DeathBehaviour>();
//         }
//     }
// }
// using System;
// using UnityEngine;
//
// // ReSharper disable FieldCanBeMadeReadOnly.Global
//
// namespace Atomic.Entities
// {
//     [AddComponentMenu("")]
//     public class SceneEntityBakerTestDouble : EntityBaker<IEntity>
//     {
//         public static int CreateCallCount;
//
//         public Func<IEntity> CreateMethod = () => new Entity();
//
//         protected override IEntity Create(
//             in int tagCapacity,
//             in int valueCapacity,
//             in int behaviourCapacity,
//             in Entity.Settings settings
//         )
//         {
//             CreateCallCount++;
//             return CreateMethod.Invoke(); // простая пустая сущность
//         }
//     }
// }
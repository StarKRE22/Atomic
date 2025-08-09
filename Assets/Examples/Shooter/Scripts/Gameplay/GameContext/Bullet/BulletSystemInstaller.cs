// using System;
// using Atomic.Contexts;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class BulletSystemInstaller : IContextInstaller<IGameContext>
//     {
//         [SerializeField]
//         private SceneEntity _bulletPrefab;
//
//         [SerializeField]
//         private int _initialCount;
//
//         [SerializeField]
//         private Transform _poolTransform;
//
//         public void Install(IGameContext context)
//         {
//             IEntityPool pool = new SceneEntityPool(_bulletPrefab, _poolTransform);
//             context.AddBulletPool(pool);
//             context.WhenInit(() => pool.Init(_initialCount));
//         }
//     }
// }
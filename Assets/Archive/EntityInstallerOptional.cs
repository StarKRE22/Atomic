// using System;
// using System.Runtime.CompilerServices;
// using UnityEngine;
//
// namespace Atomic.Entities
// {
//     [Serializable]
//     public abstract class EntityInstallerOptional : IEntityInstaller
//     {
//         [SerializeField]
//         private bool _active = true;
//
//         void IEntityInstaller.Install(IEntity entity)
//         {
//             if (_active) 
//                 this.Install(entity);
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         protected abstract void Install(IEntity entity);
//     }
//
//     public abstract class EntityInstallerOptional<E> : EntityInstallerOptional where E : IEntity
//     {
//         protected sealed override void Install(IEntity entity) => this.Install((E) entity);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         protected abstract void Install(E entity);
//     }
// }
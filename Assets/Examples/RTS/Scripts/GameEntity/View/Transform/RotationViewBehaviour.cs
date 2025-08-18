// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class RotationViewBehaviour : IEntityInit, IEntityDispose
//     {
//         private readonly Transform _transform;
//         private IReactiveValue<Quaternion> _rotation;
//
//         public RotationViewBehaviour(Transform transform)
//         {
//             _transform = transform;
//         }
//
//         public void Init(in IEntity entity)
//         {
//             _rotation = entity.GetRotation();
//             _rotation.Observe(this.OnRotationChanged);
//         }
//
//         public void Dispose(in IEntity entity)
//         {
//             _rotation.Unsubscribe(this.OnRotationChanged);
//         }
//
//         private void OnRotationChanged(Quaternion rotation)
//         {
//             _transform.rotation = rotation;
//         }
//     }
// }
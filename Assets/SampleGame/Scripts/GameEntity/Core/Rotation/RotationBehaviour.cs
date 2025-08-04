using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public class RotationBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private static readonly float3 UP = new(0, 1, 0);

        private Transform _transform;
        private IValue<float> _rotationSpeed;
        private IValue<float3> _rotationDirection;

        public void OnSpawn(IGameEntity entity)
        {
            _transform = entity.GetTransform();
            _rotationSpeed = entity.GetRotationSpeed();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            float3 direction = _rotationDirection.Value;
            if (math.lengthsq(direction) < 1e-6f)
                return;

            float3 forward = math.normalize(direction);
            quaternion current = _transform.rotation;
            quaternion target = quaternion.LookRotation(forward, UP);

            float maxRadians = _rotationSpeed.Value * deltaTime;
            float angle = math.degrees(math.acos(math.clamp(math.dot(current.value, target.value), -1f, 1f))) * 2f;

            if (angle < 1e-3f) // уже почти совпали
            {
                _transform.rotation = target;
                return;
            }

            float t = math.saturate(maxRadians / math.radians(angle));
            _transform.rotation = math.slerp(current, target, t);
        }
    }
}


// float3 direction = _rotationDirection.Value;
// if (math.lengthsq(direction) < 1e-6f) // избегаем NaN в LookRotation
//     return;
//
// quaternion current = _transform.rotation;
// quaternion target = quaternion.LookRotation(direction, UP);
//
// float angleStep = _rotationSpeed.Value * deltaTime;
// _transform.rotation = math.slerp(current, target, math.saturate(angleStep));
//
//

// float3 direction = _rotationDirection.Value;
// if (direction.Equals(float3.zero))
//     return;
//
// quaternion targetRotation = quaternion.LookRotation(direction, UP);
// float step = _rotationSpeed.Value * deltaTime;
// _transform.rotation = math.slerp(_transform.rotation, targetRotation, step);

// public sealed class RotationBehaviour : IEntityInit, IEntityFixedUpdate
//     {
//         private IVariable<quaternion> _rotation;
//         private IValue<float> _angularSpeed;
//         private IValue<float3> _moveDirection;
//
//         public void Init(IEntity entity)
//         {
//             _rotation = entity.GetRotation();
//             _angularSpeed = entity.GetAngularSpeed();
//             _moveDirection = entity.GetMoveDirection();
//         }
//
//         public void OnFixedUpdate(IEntity entity, float deltaTime)
//         {
//             RotationFunctions.RotateStep(
//                 _rotation.Value,
//                 _moveDirection.Value,
//                 deltaTime,
//                 _angularSpeed.Value,
//                 out quaternion newRotation
//             );
//             _rotation.Value = newRotation;
//         }
//     }
//
// public static void RotateStep(
// //             in quaternion rotation,
// //             in float3 direction,
// //             in float deltaTime,
// //             in float speed,
// //             out quaternion newRotation
// //         )
// //         {
// //            
// //         }
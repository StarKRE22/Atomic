using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class MoveUseCase
    {
        public static bool MoveAbstract(in IEntity entity, in Vector3 direction, in float deltaTime)
        {
            if (!entity.GetMoveCondition().Invoke(direction, deltaTime))
                return false;

            entity.GetMoveAction().Invoke(direction, deltaTime);
            entity.GetMoveEvent().Invoke(direction, deltaTime);
            return true;
        }


        public static void MoveStep(in IEntity entity, in Vector3 direction, in float deltaTime)
        {
            IVariable<Vector3> position = entity.GetPosition();
            IValue<float> speed = entity.GetMoveSpeed();
            position.Value += direction * (speed.Invoke() * deltaTime);
        }
    }
}
using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    public static class AttackUseCase
    {
        public static void Attack(this IGameEntity entity)
        {
            IValue<float> scale = entity.GetScale();
            IReactiveVariable<IGameEntity> targetVar = entity.GetTarget();

            IGameEntity target = targetVar.Value;
            if (target is not {Enabled: true} || !target.IsAlive())
                return;

            Vector3 vector = entity.GetDistanceVector(target);

            float fullDistance = entity.GetFireDistance().Value + scale.Value + target.GetScale().Value;
            if (vector.magnitude > fullDistance)
                entity.GetMoveRequest().Invoke(vector.normalized);
            else
                entity.GetFireRequest().Invoke(target);
        }
    }
}
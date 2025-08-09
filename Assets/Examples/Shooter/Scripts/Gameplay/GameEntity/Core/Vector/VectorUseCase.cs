using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class VectorUseCase
    {
        public static Vector3 GetDirectionAt(IGameEntity entity, in IGameEntity target)
        {
            Vector3 currentPosition = entity.GetPosition().Value;
            Vector3 targetPosition = target.GetPosition().Value;
            Vector3 vector = targetPosition - currentPosition;
            vector.y = 0;
            
            return vector.normalized;
        }
    }
}
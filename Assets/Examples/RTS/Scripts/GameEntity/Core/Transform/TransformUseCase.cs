using UnityEngine;

namespace RTSGame
{
    public static class TransformUseCase
    {
        public static Vector3 GetVector(IGameEntity from, IGameEntity to)
        {
            Vector3 startPosition = from.GetPosition().Value;
            Vector3 endPosition = to.GetPosition().Value;
            return endPosition - startPosition;
        }

        public static Vector3 GetForward(IGameEntity entity)
        {
            return entity.GetRotation().Value * Vector3.forward;
        }
    }
}
using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class KinematicMovementBehaviour : IGameEntityInit, IGameEntityFixedTick
    {
        private readonly IValue<float> _maxDistance;
        private readonly IValue<LayerMask> _layerMask;
        private IVariable<Vector3> _position;

        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        private IFunction<bool> _moveCondition;
        private IEvent<Vector3> _moveEvent;

        public KinematicMovementBehaviour(IValue<float> maxDistance, IValue<LayerMask> layerMask)
        {
            _maxDistance = maxDistance;
            _layerMask = layerMask;
        }

        public void Init(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMovementSpeed();
            _moveDirection = entity.GetMovementDirection();
            _moveCondition = entity.GetMovementCondition();
            _moveEvent = entity.GetMovementEvent();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction == Vector3.zero || !_moveCondition.Invoke())
                return;

            Vector3 position = _position.Value;

            bool hasObstacle = Physics.Raycast(position, direction, _maxDistance.Value, _layerMask.Value, 
                QueryTriggerInteraction.Ignore);
            
            if (hasObstacle) 
                return;

            _position.Value += direction * (_moveSpeed.Value * deltaTime); 
            _moveEvent.Invoke(direction);
        }
    }
}
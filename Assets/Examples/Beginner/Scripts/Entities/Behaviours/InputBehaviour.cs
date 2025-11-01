using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class InputBehaviour : IEntityInit, IEntityTick
    {
        private InputMap _inputMap;
        private IVariable<Vector3> _moveDirection;

        public void Init(IEntity entity)
        {
            _inputMap = entity.GetInputMap();
            _moveDirection = entity.GetMovementDirection();
        }

        public void Tick(IEntity entity, float deltaTime)
        {
            _moveDirection.Value = this.ReadMoveDirection();
        }

        private Vector3 ReadMoveDirection()
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey(_inputMap.left))
                moveDirection.x = -1;
            else if (Input.GetKey(_inputMap.right))
                moveDirection.x = 1;

            if (Input.GetKey(_inputMap.back))
                moveDirection.z = -1;
            else if (Input.GetKey(_inputMap.forward))
                moveDirection.z = 1;

            return moveDirection;
        }
    }
}
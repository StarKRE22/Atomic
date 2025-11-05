using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Handles player input and updates the movement direction of an entity based on keyboard keys.
    /// </summary>
    /// <remarks>
    /// This behaviour continuously reads the keyboard input defined in the <see cref="InputMap"/> asset
    /// and writes the resulting direction vector into the entity's movement variable.
    /// </remarks>
    /// <seealso cref="InputMap"/>
    /// <seealso cref="IVariable{T}"/>
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
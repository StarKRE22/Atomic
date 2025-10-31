using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterMoveController : IPlayerContextInit, IPlayerContextTick
    {
        private IEntity _character;
        private InputMap _inputMap;

        public void Init(PlayerContext entity)
        {
            _character = entity.GetCharacter();
            _inputMap = entity.GetInputMap();
        }

        public void Tick(PlayerContext entity, float deltaTime)
        {
            _character.GetMoveDirection().Value = this.GetMoveDirection();
        }

        private float3 GetMoveDirection()
        {
            float3 moveDirection = float3.zero;

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
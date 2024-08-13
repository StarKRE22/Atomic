using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Walkthrough
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private SceneEntity character;

        private void Update()
        {
            this.character.GetMoveDirection().Value = this.GetMoveDirection();
        }

        private float3 GetMoveDirection()
        {
            float3 moveDirection = float3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection.x = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection.x = -1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection.z = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection.z = -1;
            }

            return moveDirection;
        }
    }
}
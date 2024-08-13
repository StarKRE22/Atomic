using Atomic.Entities;
using Atomic.Extensions;
using GameExample.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Content
{
    public sealed class CharacterEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField]
        private float moveSpeed = 3;

        [SerializeField]
        private float angularSpeed = 12;

        [SerializeField]
        private float3 initialDirection;
        
        [SerializeField]
        private TriggerEventReceiver triggerEventReceiver;

        public override void Install(IEntity entity)
        {
            entity.AddCharacterTag();
            entity.AddGameObject(this.gameObject);
            entity.AddTransform(this.transform);

            entity.AddPosition(new float3Reactive(this.transform.position));
            entity.AddRotation(new quaternionReactive(this.transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();

            entity.AddMoveSpeed(this.moveSpeed);
            entity.AddMoveDirection(this.initialDirection);
            entity.AddBehaviour<MovementBehaviour>();

            entity.AddAngularSpeed(this.angularSpeed);
            entity.AddBehaviour<RotationBehaviour>();
            
            entity.AddTriggerEventReceiver(this.triggerEventReceiver);
        }
    }
}
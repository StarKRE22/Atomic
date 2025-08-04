using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 360 * 4;

        [SerializeField]
        private TriggerEvents _triggerEvents;
        
        protected override void Install(IGameEntity entity)
        {
            entity.AddCharacterTag();
            entity.AddBehaviour<CharacterMoveBehaviour>();
            
            entity.AddGameObject(this.gameObject);
            entity.AddTransform(this.transform);
            entity.AddTriggerEvents(_triggerEvents);
            
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveDirection(new BasicVariable<float3>());
            entity.AddBehaviour<MovementBehaviour>();
            
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddRotationDirection(new BasicVariable<float3>());
            entity.AddBehaviour<RotationBehaviour>();
            
            entity.AddBehaviour<CharacterMoveBehaviour>();
            entity.AddBehaviour<CharacterPickUpBehaviour>();
        }
    }
}
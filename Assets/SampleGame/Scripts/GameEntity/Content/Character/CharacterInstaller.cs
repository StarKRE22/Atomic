using Atomic.Elements;
using Atomic.Entities;
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
            //Character:
            entity.AddCharacterTag();
            entity.AddBehaviour<CharacterMoveBehaviour>();
            entity.AddBehaviour<CharacterPickUpBehaviour>();
            
            //Base:
            entity.AddGameObject(this.gameObject);
            entity.AddTransform(this.transform);
            entity.AddTriggerEvents(_triggerEvents);
            entity.AddTeamType(new ReactiveVariable<TeamType>());
            
            //Movement:
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveDirection(new BasicVariable<Vector3>());
            entity.AddBehaviour<MovementBehaviour>();
            
            //Rotation:
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddRotationDirection(new BasicVariable<Vector3>());
            entity.AddBehaviour<RotationBehaviour>();
        }
    }
}
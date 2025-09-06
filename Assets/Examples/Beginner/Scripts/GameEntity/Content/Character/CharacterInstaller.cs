using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 360 * 4;

        [SerializeField]
        private TriggerEvents _triggerEvents;

        public override void Install(IGameEntity entity)
        {
            //Character:
            entity.AddCharacterTag();
            entity.AddBehaviour<CharacterMoveBehaviour>();
            entity.AddBehaviour<CharacterPickUpBehaviour>();
            entity.AddBehaviour<CharacterNameBehaviour>();

            //Base:
            entity.AddPosition(new TransformPositionVariable(this.transform));
            entity.AddRotation(new TransformRotationVariable(this.transform));
            entity.AddTeamType(new ReactiveVariable<TeamType>());
            entity.AddTriggerEvents(_triggerEvents);

            //Movement:
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveDirection(new BaseVariable<Vector3>());
            entity.AddBehaviour<MovementBehaviour>();

            //Rotation:
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddRotationDirection(new BaseVariable<Vector3>());
            entity.AddBehaviour<RotationBehaviour>();
        }
    }
}
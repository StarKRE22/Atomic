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
            entity.AddPosition(new ProxyVariable<Vector3>(
                getter: () => this.transform.position,
                setter: value => this.transform.position = value)
            );
            entity.AddRotation(new ProxyVariable<Quaternion>(
                getter: () => this.transform.rotation,
                setter: value => this.transform.rotation = value)
            );
            entity.AddTriggerEvents(_triggerEvents);
            entity.AddTeamType(new ReactiveVariable<TeamType>());

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
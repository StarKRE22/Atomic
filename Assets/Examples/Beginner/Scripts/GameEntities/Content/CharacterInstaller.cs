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
        
        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private MoneyView _moneyView;
        
        [SerializeField]
        private Transform _canvas;

        public override void Install(IGameEntity entity)
        {
            //Team:
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
            
            //Money:
            entity.AddMoneyView(_moneyView);
            entity.AddBehaviour<MoneyPresenter>();
            
            //Billboard:
            entity.AddBehaviour(new CameraBillboardBehaviour(_canvas));
        }

        public override void Install(IGameEntity entity)
        {
            //Character:
            entity.AddCharacterTag();
            entity.AddBehaviour<MoveBehaviour>();
            entity.AddBehaviour<CollectBehaviour>();
            entity.AddBehaviour<NameBehaviour>();

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

        public override void Uninstall(IGameEntity entity)
        {
            
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private TriggerEvents _triggerEvents;

        [SerializeField]
        private InputMap _inputMap;

        [SerializeField]
        private ReactiveVariable<int> _money;

        public override void Install(IEntity entity)
        {
            // Common
            entity.AddCharacterTag();
            
            entity.AddTransform(this.transform);
            entity.AddTriggerEvents(_triggerEvents);
            entity.AddMoney(_money);

            entity.AddBehaviour<CoinCollectBehaviour>();

            // Movement
            entity.AddMovementSpeed(_moveSpeed);
            entity.AddMovementDirection(new BaseVariable<Vector3>());
            entity.AddBehaviour<MovementBehaviour>();

            // Input
            entity.AddInputMap(_inputMap);
            entity.AddBehaviour<InputBehaviour>();
        }
    }
}
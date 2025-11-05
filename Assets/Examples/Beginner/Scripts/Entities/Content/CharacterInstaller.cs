using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Installs all atomic elements, values, and behaviours required to build a controllable character entity.
    /// </summary>
    /// <remarks>
    /// This installer composes the character entity by providing movement, input, and coin collection logic.
    /// It wires up all required components, including <see cref="Transform"/>, input bindings,
    /// reactive money counter, and movement speed configuration.
    ///
    /// <para>
    /// The installer uses the <see cref="SceneEntityInstaller"/> base class to register components
    /// directly into the entity lifecycle at scene initialization.
    /// </para>
    /// </remarks>
    /// <seealso cref="MovementBehaviour"/>
    /// <seealso cref="InputBehaviour"/>
    /// <seealso cref="CoinCollectBehaviour"/>
    /// <seealso cref="SceneEntityInstaller"/>
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
            entity.AddMovementDirection(new Variable<Vector3>());
            entity.AddBehaviour<MovementBehaviour>();

            // Input
            entity.AddInputMap(_inputMap);
            entity.AddBehaviour<InputBehaviour>();
        }
    }
}
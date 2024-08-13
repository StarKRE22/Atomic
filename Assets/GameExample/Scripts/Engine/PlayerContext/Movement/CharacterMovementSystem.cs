using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;

namespace GameExample.Engine
{
    public sealed class CharacterMovementSystem : IContextInit, IContextUpdate
    {
        private IValue<IEntity> _character;
        private InputMap _inputMap;

        public void Init(IContext context)
        {
            _character = context.GetCharacter();
            _inputMap = context.GetInputMap();
        }

        public void Update(IContext context, float deltaTime)
        {
            _character.Value.GetMoveDirection().Value = _inputMap.GetMoveDirection();
        }
    }
}





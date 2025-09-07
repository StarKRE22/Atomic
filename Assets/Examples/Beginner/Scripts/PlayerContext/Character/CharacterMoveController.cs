using Atomic.Entities;
using Unity.Mathematics;

namespace BeginnerGame
{
    public sealed class CharacterMoveController : IEntityInit<IPlayerContext>, IEntityTick
    {
        private IGameEntity _character;
        private InputMap _inputMap;
        
        public void Init(IPlayerContext entity)
        {
            _character = entity.GetCharacter();
            _inputMap = entity.GetInputMap();
        }

        public void Tick(IEntity entity, float deltaTime)
        {
            float3 moveDirection = InputUseCase.GetMoveDirection(_inputMap);
            _character.GetMoveDirection().Value = moveDirection;
        }
    }
}
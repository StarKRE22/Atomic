using Atomic.Entities;
using Unity.Mathematics;

namespace BeginnerGame
{
    public sealed class CharacterMoveController : IEntitySpawn<IPlayerContext>, IEntityUpdate
    {
        private IGameEntity _character;
        private InputMap _inputMap;
        
        public void OnSpawn(IPlayerContext entity)
        {
            _character = entity.GetCharacter();
            _inputMap = entity.GetInputMap();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            float3 moveDirection = InputUseCase.GetMoveDirection(_inputMap);
            _character.GetMoveDirection().Value = moveDirection;
        }
    }
}
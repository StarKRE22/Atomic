using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class PlayerContextInstaller : SceneEntityInstaller<IPlayerContext>
    {
        [SerializeField]
        private TeamType _teamType;

        [SerializeField]
        private InputMap _inputMap;

        [SerializeField]
        private ReactiveVariable<int> _money;

        [Header("Character")]
        [SerializeField]
        private GameEntity _characterPrefab;

        [SerializeField]
        private Transform _spawnPoint;

        [Header("Camera")]
        [SerializeField]
        private Transform _cameraRoot;

        [SerializeField]
        private Vector3 _cameraOffset;

        protected override void Install(IPlayerContext context)
        {
            GameContext gameContext = GameContext.Instance;
            gameContext.GetPlayers().Add(_teamType, context);

            //Base:
            context.AddTeamType(new Const<TeamType>(_teamType));
            context.AddInputMap(_inputMap);
            context.AddMoney(_money);

            //Character:
            GameEntity character = CharactersUseCase.Spawn(gameContext, _characterPrefab, _spawnPoint, _teamType);
            context.AddCharacter(character);
            context.AddBehaviour<CharacterMoveController>();

            //Camera:
            context.AddBehaviour(new CameraFollowController(_cameraRoot, _cameraOffset));
        }
    }
}
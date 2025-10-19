using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class PlayerContextInstaller : SceneEntityInstaller<IPlayerContext>
    {
        [SerializeField]
        private Const<TeamType> _teamType;

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
        private Camera _camera;

        [SerializeField]
        private Vector3 _cameraOffset;

        public override void Install(IPlayerContext context)
        {
            GameContext gameContext = GameContext.Instance;
            if (AtomicUtils.IsPlayMode())
                gameContext.GetPlayers().Add(_teamType, context);

            //Base:
            context.AddTeamType(_teamType);
            context.AddInputMap(_inputMap);
            context.AddMoney(_money);
            context.AddCamera(_camera);

            if (AtomicUtils.IsPlayMode())
            {
                //Character:
                GameEntity character = CharactersUseCase.Spawn(gameContext, _characterPrefab, _spawnPoint, _teamType);
                context.AddCharacter(character);
                context.AddBehaviour<CharacterMoveController>();
            }

            //Camera:
            context.AddBehaviour(new CameraFollowController(_cameraOffset));

            //Inactivate
            if (AtomicUtils.IsPlayMode())
            {
                gameContext.WhenDisable(context.Disable);
                gameContext.WhenDisable(context.GetCharacter().Disable);
            }
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class PlayerContextInstaller : SceneEntityInstaller<PlayerContext>
    {
        [SerializeField]
        private Const<TeamType> _teamType;

        [SerializeField]
        private InputMap _inputMap;

        [SerializeField]
        private ReactiveVariable<int> _money;

        [Header("Character")]
        [SerializeField]
        private SceneEntity _character;

        [SerializeField]
        private Transform _spawnPoint;

        [Header("Camera")]
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Vector3 _cameraOffset;

        public override void Install(PlayerContext playerContext)
        {
            //Base:
            playerContext.AddTeamType(_teamType);
            playerContext.AddInputMap(_inputMap);
            playerContext.AddMoney(_money);

            //Character
            playerContext.AddCharacter(_character);
            playerContext.AddBehaviour<CharacterMoveController>();
            playerContext.WhenDisable(_character.Disable);

            //Camera:
            playerContext.AddCamera(_camera);
            playerContext.AddBehaviour(new CameraFollowController(_cameraOffset));

            this.InstallGameContext(playerContext);
        }
        
        private void InstallGameContext(PlayerContext playerContext)
        {
            if (!AtomicUtils.IsPlayMode())
                return;

            GameContext gameContext = GameContext.Instance;
            gameContext.GetPlayers().Add(_teamType, playerContext);
            gameContext.WhenDisable(playerContext.Disable);
        }
    }
}
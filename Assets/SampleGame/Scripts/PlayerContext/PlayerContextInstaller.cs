using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public class PlayerContextInstaller : SceneEntityInstaller<IPlayerContext>
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
            
            context.AddTeamType(new Const<TeamType>(_teamType));
            context.AddInputMap(_inputMap);
            context.AddMoney(_money);

            GameEntity character = EntitiesUseCase.Spawn(gameContext, _characterPrefab, _spawnPoint, _teamType);
            context.AddCharacter(character);
            context.AddBehaviour<CharacterMoveController>();
            
            context.AddBehaviour(new CameraFollowController(_cameraRoot, _cameraOffset));
        }
    }
}


//
//
//         public override void Install(IContext context)
//         {
//             context.GetPlayerMap().Add(this.teamType, context);
//             context.AddTeamType(this.teamType);
//
//             context.AddCharacter(new Const<IEntity>(this.character));
//             context.AddInputMap(this.inputMap);
//             context.AddCameraData(this.cameraData);
//             
//             context.AddSystem<CharacterMovementSystem>();
//             context.AddSystem<CoinCollectSystem>();
//             context.AddSystem<CameraFollowSystem>();
//         }
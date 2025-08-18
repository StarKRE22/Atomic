// using UnityEngine;
//
// namespace RTSGame
// {
//     [DefaultExecutionOrder(-1000)]
//     public sealed class EntryPoint : MonoBehaviour
//     {
//         [SerializeField]
//         private GameContextInstaller _gameInstaller;
//
//         [SerializeField]
//         private PlayerContextInstaller _playerInstaller;
//
//         [SerializeField]
//         private EntityWorldView _entityWorldView;
//         
//         private void Start()
//         {
//             var gameContext = this.CreateGameContext();
//             this.CreatePlayerContext(gameContext, TeamType.BLUE);
//             this.CreatePlayerContext(gameContext, TeamType.RED);
//             
//             LevelUseCase.LoadLevel(gameContext);
//             
//        
//
//             _contextRunner.Init();
//             _contextRunner.Enable();
//             
//             _entityWorldView.Show(gameContext.GetEntityWorld());
//         }
//
//         private GameContext CreateGameContext()
//         {
//             GameContext gameContext = GameContext.Instance;
//             _gameInstaller.Install(gameContext);
//             _contextRunner.Add(gameContext);
//             return gameContext;
//         }
//
//         private void CreatePlayerContext(GameContext gameContext, TeamType teamType)
//         {
//             PlayerContext playerContext = new PlayerContext(teamType);
//             _playerInstaller.Install(playerContext);
//             gameContext.GetPlayers().Add(teamType, playerContext);
//             _contextRunner.Add(playerContext);
//         }
//
//         private void Update()
//         {
//             _contextRunner.OnUpdate(Time.deltaTime);
//         }
//
//         private void FixedUpdate()
//         {
//             float deltaTime = Time.fixedDeltaTime;
//             _contextRunner.OnFixedUpdate(deltaTime);
//         }
//
//         private void LateUpdate()
//         {
//             _contextRunner.OnLateUpdate(Time.deltaTime);
//         }
//
//         private void OnDestroy()
//         {
//             _contextRunner.Dispose();
//         }
//     }
// }
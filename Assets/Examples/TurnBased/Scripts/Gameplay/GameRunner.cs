using System.Collections;
using Atomic.Entities;
using Game.Gameplay;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Game
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameRunner : MonoBehaviour
    {
        public static GameRunner Instance { get; private set; }

        public IGameContext GameContext => _gameContext;

        [SerializeField]
        private GameContextFactory _gameFactory;
        
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        private IGameContext _gameContext;

        private void Awake()
        {
            _gameContext = _gameFactory.Create(NoArgs.Default);
            _gameContext.Init();
            _gameContext.Enable();
            Instance = this;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.25f);
            _gameContext.RunInMainThread(_gameContext.StartGame);
        }

        private void FixedUpdate() => _gameContext.FixedTick(Time.fixedDeltaTime);

        private void Update() => _gameContext.Tick(Time.deltaTime);

        private void LateUpdate() => _gameContext.LateTick(Time.deltaTime);

        private void OnDestroy()
        {
            _gameContext.Disable();
            _gameContext.Dispose();
        }
    }
}
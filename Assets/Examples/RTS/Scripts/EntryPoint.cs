using Sirenix.OdinInspector;
using UnityEngine;

namespace RTSGame
{
    [DefaultExecutionOrder(-1000)]
    public sealed class EntryPoint : MonoBehaviour
    {
        [ShowInInspector]
        public static IGameContext GameContext { get; private set; }

        [SerializeField]
        private GameContextFactory _gameFactory;

        [SerializeField]
        private GameEntityWorldView _entityWorldView;

        [SerializeField]
        private bool _bakingMode;

        private void Awake()
        {
            GameContext = _gameFactory.Create();
            
            if (_bakingMode)
                InitGameCase.BakeUnits(GameContext);
            else
                InitGameCase.SpawnUnits(GameContext);


            GameContext.Spawn();
            GameContext.Activate();
        }

        private void Start() => _entityWorldView.Show(GameContext.GetEntityWorld());

        private void Update() => GameContext.OnUpdate(Time.deltaTime);

        private void FixedUpdate() => GameContext.OnFixedUpdate(Time.fixedDeltaTime);

        private void LateUpdate() => GameContext.OnLateUpdate(Time.deltaTime);

        private void OnDestroy()
        {
            GameContext.Deactivate();
            GameContext.Despawn();
        }
    }
}
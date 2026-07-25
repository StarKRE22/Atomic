using Atomic.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace RTSGame
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameRunner : MonoBehaviour
    {
        public IGameContext GameContext => _gameContext;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        private IGameContext _gameContext;

        [SerializeField]
        private GameContextFactory _gameFactory;

        [Header("Baking")]
        [SerializeField]
        private bool _bakeUnits;

        [SerializeField]
        private Transform _playerPoint;

#if ODIN_INSPECTOR
        [HideIf(nameof(_bakeUnits))]
#endif
        [FormerlySerializedAs("_spawnUnits")]
        [SerializeField]
        private int _unitColumns = 100;

        private void Awake()
        {
            _gameContext = _gameFactory.Create(NoArgs.Default);
            _gameContext.AddPlayerPoint(_playerPoint);

            this.SpawnUnits();
            this.PrewarmBullets();
            _gameContext.Init();
        }

        private void PrewarmBullets()
        {
            _gameContext.GetEntityPool().Init(GameEntityType.Projectile, 4096);
        }

        private void SpawnUnits()
        {
            if (_bakeUnits)
                GameEntityBaker.BakeAll(
                    new Args<IGameContext>(_gameContext),
                    _gameContext.GetEntityWorld(),
                    includeInactive: false
                );
            else
                _gameContext.SpawnInitialUnits(_unitColumns);
        }

        private void OnEnable() => _gameContext.Enable();

        private void Update() => _gameContext.Tick(Time.deltaTime);

        private void FixedUpdate() => _gameContext.FixedTick(Time.fixedDeltaTime);

        private void LateUpdate() => _gameContext.LateTick(Time.deltaTime);

        private void OnDrawGizmos() => _gameContext?.DrawGizmos();

        private void OnDisable() => _gameContext.Disable();

        private void OnDestroy() => _gameContext.Dispose();
    }
}
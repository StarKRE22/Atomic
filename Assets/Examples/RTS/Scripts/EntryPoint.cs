using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RTSGame
{
    [DefaultExecutionOrder(-1000)]
    public sealed class EntryPoint : MonoBehaviour
    {
        [ShowInInspector, HideInEditorMode]
        private IGameContext _gameContext;

        [SerializeField]
        private GameContextFactory _gameContextFactory;

        [SerializeField]
        private EntityCollectionView _entityCollectionView;
        
        [SerializeField]
        private bool _bakeUnits;

        [HideIf(nameof(_bakeUnits))]
        [SerializeField]
        private int _spawnUnits = 100;
        
        private EntityCollectionViewBinder<IGameEntity> _viewBinder;
        
        private void Awake()
        {
            _gameContext = _gameContextFactory.Create();

            if (_bakeUnits)
                SceneEntityBaker<IGameEntity>.BakeAll(_gameContext.GetEntityWorld(), false);
            else
                InitGameCase.SpawnUnits(_gameContext, _spawnUnits);

            _gameContext.Spawn();
            _gameContext.Activate();
        }

        private void Start() => _viewBinder = new EntityCollectionViewBinder<IGameEntity>(
            _gameContext.GetEntityWorld(),
            _entityCollectionView
        );

        private void Update() => _gameContext.OnUpdate(Time.deltaTime);

        private void FixedUpdate() => _gameContext.OnFixedUpdate(Time.fixedDeltaTime);

        private void LateUpdate() => _gameContext.OnLateUpdate(Time.deltaTime);

        private void OnDestroy()
        {
            _viewBinder.Dispose();
            _gameContext.Deactivate();
            _gameContext.Despawn();
        }
    }
}
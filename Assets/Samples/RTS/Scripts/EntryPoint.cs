using Atomic.Entities;
using UnityEngine;
using UnityEngine.Serialization;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace RTSGame
{
    [DefaultExecutionOrder(-1000)]
    public sealed class EntryPoint : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        private IGameContext _gameContext;

        [SerializeField]
        private GameContextFactory _gameContextFactory;

        [SerializeField]
        private EntityCollectionView _entityCollectionView;

        [Header("Visualization")]
        [SerializeField]
        private bool _showEntityViews = true;

        [Header("Baking")]
        [SerializeField]
        private bool _bakeUnits;

#if ODIN_INSPECTOR
        [ShowIf(nameof(_bakeUnits))]
#endif
        [SerializeField]
        private bool _bakeIncludeInactive;

#if ODIN_INSPECTOR
        [HideIf(nameof(_bakeUnits))]
#endif
        [FormerlySerializedAs("_spawnUnits")]
        [SerializeField]
        private int _unitColumns = 100;

        private EntityCollectionViewBinder<IGameEntity> _viewBinder;

        private void Start()
        {
            _gameContext = _gameContextFactory.Create();
            this.SpawnUnits();
            _gameContext.Init();
            _gameContext.Enable();
            this.BindEntityViews();
        }
        
        private void SpawnUnits()
        {
            if (_bakeUnits)
                SceneEntityBaker<IGameEntity>.BakeAll(_gameContext.GetEntityWorld(), _bakeIncludeInactive);
            else
                InitGameCase.SpawnUnits(_gameContext, _unitColumns);
        }

        private void BindEntityViews()
        {
            if (!_showEntityViews)
                return;

            _viewBinder = new EntityCollectionViewBinder<IGameEntity>(
                _gameContext.GetEntityWorld(), _entityCollectionView
            );
        }

        private void Update() => _gameContext.OnUpdate(Time.deltaTime);

        private void FixedUpdate() => _gameContext.OnFixedUpdate(Time.fixedDeltaTime);

        private void LateUpdate() => _gameContext.OnLateUpdate(Time.deltaTime);

        private void OnDestroy()
        {
            _viewBinder?.Dispose();
            _gameContext?.Dispose();
        }
    }
}
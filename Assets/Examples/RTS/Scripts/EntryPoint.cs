using System;
using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        [Header("Visualization")]
        [SerializeField]
        private bool _showEntityViews = true;

        [Header("Baking")]
        [SerializeField]
        private bool _bakeUnits;

        [ShowIf(nameof(_bakeUnits))]
        [SerializeField]
        private bool _bakeIncludeInactive;

        [HideIf(nameof(_bakeUnits))]
        [FormerlySerializedAs("_spawnUnits")]
        [SerializeField]
        private int _unitColumns = 100;

        private EntityCollectionViewBinder<IGameEntity> _viewBinder;

        private void Start()
        {
            _gameContext = _gameContextFactory.Create();
            this.SpawnUnits();
            _gameContext.Spawn();
            _gameContext.Activate();
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
            _gameContext.Deactivate();
            _gameContext.Despawn();
        }
    }
}
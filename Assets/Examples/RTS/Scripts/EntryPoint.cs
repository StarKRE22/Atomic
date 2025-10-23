using System;
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

        [FormerlySerializedAs("_entityCollectionView")]
        [SerializeField]
        private UnitCollectionView _entityWorldView;

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

        private void Awake()
        {
            GameContext.DropInstance();
            GameContext.SetFactory(_gameContextFactory);
            _gameContext = GameContext.Instance;
        }

        private void Start()
        {
            this.SpawnUnits();
            
            _gameContext.Init();
            _gameContext.Enable();
            
            if (_showEntityViews)
                _entityWorldView.Show(_gameContext.GetEntityWorld());
        }
        
        private void SpawnUnits()
        {
            if (_bakeUnits)
                UnitBaker.BakeAll(_gameContext.GetEntityWorld(), _bakeIncludeInactive);
            else
                InitGameCase.SpawnUnits(_gameContext, _unitColumns);
        }

        private void Update() => _gameContext.Tick(Time.deltaTime);

        private void FixedUpdate() => _gameContext.FixedTick(Time.fixedDeltaTime);

        private void LateUpdate()
        {
            _gameContext.LateTick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (_showEntityViews)
                _entityWorldView.Hide();
            
            GameContext.DropInstance();
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class UIContextInstaller : MonoEntityInstaller<IUIContext>
    {
        [SerializeField]
        private Transform _poolContainer;

        [SerializeField]
        private GameObject _waterSplash;

        [SerializeField]
        private GameObject _deathEffect;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GameEntityCollectionView _gameEntityViews;

        [SerializeField]
        private SelectedMarkerView _markerView;

        [SerializeField]
        private GameBoardInstaller _gameBoardInstaller;

        [SerializeField]
        private HUDInstaller _uiInstaller;

        [SerializeField]
        private InputInstaller _inputInstaller;

        public override void Install(IUIContext ui)
        {
            IGameContext gameContext = GameRunner.Instance.GameContext;

            ui.AddValue(UIContextAPI.Camera, _camera);
            ui.AddValue(UIContextAPI.GameObjectPrefabPool, new GameObjectPool(_poolContainer));
            ui.AddValue(UIContextAPI.WaterSplashEffect, _waterSplash);
            ui.AddValue(UIContextAPI.DeathEffect, _deathEffect);
            
            ui.AddValue(UIContextAPI.EntityCollectionView, _gameEntityViews);
            
            _gameBoardInstaller.Install(ui, gameContext);
            _uiInstaller.Install(ui, gameContext);
            _inputInstaller.Install(ui, gameContext);
            this.InstallSelection(ui, gameContext);
            this.InstallGameEvents(ui, gameContext);
        }

        private void InstallSelection(IUIContext ui, IGameContext gameContext)
        {
            ui.AddValue(UIContextAPI.SelectedCharacter, new ReactiveVariable<IGameEntity>());
            ui.AddBehaviour(new SelectedMarkerPresenter(_markerView));
            ui.AddBehaviour(new SelectionDropController(gameContext));
            ui.AddBehaviour(new SelectionCellsPresenter(gameContext));
        }

        private void InstallGameEvents(IUIContext ui, IGameContext gameContext)
        {
            ui.AddValue(UIContextAPI.CommandQueue, new UICommandQueue());
            
            ui.AddBehaviour(new TakeDamageEntityObserver(gameContext));
            ui.AddBehaviour(new MoveEntityObserver(gameContext));
            ui.AddBehaviour(new AttackEntityObserver(gameContext));
            ui.AddBehaviour(new EnemyTurnEndObserver(gameContext));
            
            ui.AddBehaviour(new PushEntityObserver(gameContext));
            ui.AddBehaviour(new DieEntityObserver(gameContext));
            ui.AddBehaviour(new SpawnEntityObserver(gameContext));
        }
    }
}
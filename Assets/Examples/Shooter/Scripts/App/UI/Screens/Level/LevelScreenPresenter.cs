using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class LevelScreenPresenter : 
        IEntitySpawn<IMenuUIContext>, 
        IEntityDespawn,
        IEntityActivate,
        IEntityDeactivate
    {
        private readonly LevelScreenView _screenView;
        private IMenuUIContext _uiContext;
        private IAppContext _appContext;

        public LevelScreenPresenter(LevelScreenView screenView)
        {
            _screenView = screenView;
        }

        public void OnSpawn(IMenuUIContext context)
        {
            _appContext = AppContext.Instance;
            _uiContext = context;
            this.SpawnLevelItems();
        }
        
        public void OnActivate(IEntity entity)
        {
            _screenView.OnCloseClicked += this.OnCloseClicked;
        }

        public void OnDeactivate(IEntity entity)
        {
            _screenView.OnCloseClicked -= this.OnCloseClicked;
        }

        private void SpawnLevelItems()
        {
            int startLevel = _appContext.GetStartLevel().Value;
            int maxLevel = _appContext.GetMaxLevel().Value;
            for (int i = startLevel; i <= maxLevel; i++)
            {
                LevelItemView itemView = _screenView.CreateItem();
                LevelItemPresenter itemPresenter = new LevelItemPresenter(_appContext, i, itemView);
                _uiContext.AddBehaviour(itemPresenter);
            }
        }
        
        public void OnDespawn(IEntity entity)
        {
            _uiContext.DelAllBehaviours<LevelItemPresenter>();
            _screenView.ClearAllItems();
        }

        private void OnCloseClicked() => ScreenUseCase.ShowScreen<StartScreenView>(_uiContext);
    }
}
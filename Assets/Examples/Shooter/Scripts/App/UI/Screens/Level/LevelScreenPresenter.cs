using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class LevelScreenPresenter : 
        IEntityInit<IMenuUIContext>, 
        IEntityDispose,
        IEntityEnable,
        IEntityDisable
    {
        private readonly LevelScreenView _screenView;
        private IMenuUIContext _uiContext;
        private IAppContext _appContext;

        public LevelScreenPresenter(LevelScreenView screenView)
        {
            _screenView = screenView;
        }

        public void Init(IMenuUIContext context)
        {
            _appContext = AppContext.Instance;
            _uiContext = context;
            this.SpawnLevelItems();
        }
        
        public void Enable(IEntity entity)
        {
            _screenView.OnCloseClicked += this.OnCloseClicked;
        }

        public void Disable(IEntity entity)
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
        
        public void Dispose(IEntity entity)
        {
            _uiContext.DelBehaviours<LevelItemPresenter>();
            _screenView.ClearAllItems();
        }

        private void OnCloseClicked() => ScreenUseCase.ShowScreen<StartScreenView>(_uiContext);
    }
}
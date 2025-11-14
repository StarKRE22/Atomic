using ShooterGame.App;

namespace ShooterGame.UI
{
    public sealed class LevelScreenPresenter :
        IMenuUIInit,
        IMenuUIDispose,
        IMenuUIEnable,
        IMenuUIDisable
    {
        private readonly IAppContext _appContext;
        private readonly LevelScreenView _screenView;
        private IMenuUI _uiContext;

        public LevelScreenPresenter(LevelScreenView screenView, IAppContext appContext)
        {
            _screenView = screenView;
            _appContext = appContext;
        }

        public void Init(IMenuUI context)
        {
            _uiContext = context;
            this.SpawnLevelItems();
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

        public void Enable(IMenuUI entity)
        {
            _screenView.OnCloseClicked += this.OnCloseClicked;
        }

        public void Disable(IMenuUI entity)
        {
            _screenView.OnCloseClicked -= this.OnCloseClicked;
        }

        private void OnCloseClicked() => ScreenUseCase.ShowScreen<StartScreenView>(_uiContext);

        public void Dispose(IMenuUI entity)
        {
            _uiContext.DelBehaviours<LevelItemPresenter>();
            _screenView.ClearAllItems();
        }
    }
}
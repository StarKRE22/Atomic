using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class LevelItemPresenter : IEntitySpawn<IMenuUIContext>, IEntityDespawn
    {
        private readonly IAppContext _context;
        private readonly int _level;
        private readonly LevelItemView _view;
        
        public LevelItemPresenter(IAppContext context, int level, LevelItemView view)
        {
            _context = context;
            _level = level;
            _view = view;
        }

        public void OnSpawn(IMenuUIContext context)
        {
            int currentLevel = _context.GetCurrentLevel().Value;
            if (currentLevel == _level)
                _view.SetAsCurrent();
            else if (currentLevel > _level)
                _view.SetAsCompleted();
            else
                _view.SetAsNotCompleted();

            _view.SetLevel(_level.ToString());
            _view.OnClicked += this.OnClicked;
        }

        public void OnDespawn(IEntity entity)
        {
            _view.OnClicked -= this.OnClicked;
        }

        private void OnClicked() => LoadGameUseCase.StartGame(_context, _level);
    }
}
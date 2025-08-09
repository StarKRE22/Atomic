using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class LevelItemPresenter : IEntitySpawn<IAppContext>, IEntityDespawn
    {
        private readonly int _level;
        private readonly LevelItemView _view;

        private IAppContext _context;
        
        public LevelItemPresenter(int level, LevelItemView view)
        {
            _level = level;
            _view = view;
        }

        public void OnSpawn(IAppContext context)
        {
            _context = context;
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
using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class SelectedMarkerPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly SelectedMarkerView _markerView;
        
        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private GameEntityCollectionView _entityViews;
        private Subscription<IGameEntity> _subscription;

        public SelectedMarkerPresenter(SelectedMarkerView markerView)
        {
            _markerView = markerView;
        }

        public void Enable(IUIContext context)
        {
            _entityViews = context.GetEntityCollectionView();
            _selectedCharacter = context.GetSelectedCharacter();
            _subscription = _selectedCharacter.Observe(this.OnSelectedCharacterChanged);
        }

        private void OnSelectedCharacterChanged(IGameEntity entity)
        {
            if (entity == null)
            {
                _markerView.Hide();
                return;
            }

            GameEntityView view = _entityViews.Get(entity);
            _markerView.Show(view.transform);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }
    }
}
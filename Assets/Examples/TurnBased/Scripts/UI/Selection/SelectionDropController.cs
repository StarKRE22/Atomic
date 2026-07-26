using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class SelectionDropController : IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        private readonly DisposableComposite _disposables = new();
        private IVariable<IGameEntity> _selectedCharacter;

        public SelectionDropController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Enable(IUIContext ui)
        {
            _selectedCharacter = ui.GetValue(UIContextAPI.SelectedCharacter);
            IGameEventBus eventBus = _gameContext.GetValue(GameContextAPI.EventBus);
            eventBus
                .Subscribe(GameEventAPI.PlayerTurnStarted, () => _selectedCharacter.Value = null)
                .AddTo(_disposables);
            
            eventBus
                .Subscribe(GameEventAPI.PlayerTurnEnded, () => _selectedCharacter.Value = null)
                .AddTo(_disposables);
        }

        public void Disable(IUIContext entity)
        {
            _disposables.Dispose();
        }
    }
}
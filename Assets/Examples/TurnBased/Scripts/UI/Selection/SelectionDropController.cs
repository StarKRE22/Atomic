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
            _selectedCharacter = ui.GetSelectedCharacter();
            IGameEventBus eventBus = _gameContext.GetEventBus();
            eventBus
                .SubscribePlayerTurnStarted( () => _selectedCharacter.Value = null)
                .AddTo(_disposables);
            
            eventBus
                .SubscribePlayerTurnEnded( () => _selectedCharacter.Value = null)
                .AddTo(_disposables);
        }

        public void Disable(IUIContext entity)
        {
            _disposables.Dispose();
        }
    }
}
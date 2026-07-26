using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using TMPro;

namespace Game.UI
{
    public sealed class SelectedCharacterStatsPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly TMP_Text _view;

        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private IGameEntity _currentCharacter;

        public SelectedCharacterStatsPresenter(TMP_Text view)
        {
            _view = view;
        }

        public void Enable(IUIContext context)
        {
            _selectedCharacter = context.GetValue(UIContextAPI.SelectedCharacter);
            _selectedCharacter.Observe(this.OnCharacterChanged);
        }
        
        public void Disable(IUIContext entity)
        {
            _selectedCharacter.Unsubscribe(this.OnCharacterChanged);
        }
        
        private void OnCharacterChanged(IGameEntity selectedCharacter)
        {
            if (_currentCharacter != null)
            {
               _currentCharacter.GetValue(GameEntityAPI.CurrentMovesCount).Unsubscribe(this.OnMovesChanged);
               _currentCharacter.GetValue(GameEntityAPI.CurrentAttacksCount).Unsubscribe(this.OnAttacksChanged);
            }
            
            _currentCharacter = selectedCharacter;
            
            if (_currentCharacter != null)
            {
                _currentCharacter.GetValue(GameEntityAPI.CurrentMovesCount).Subscribe(this.OnMovesChanged);
                _currentCharacter.GetValue(GameEntityAPI.CurrentAttacksCount).Subscribe(this.OnAttacksChanged);
            }

            _view.gameObject.SetActive(_currentCharacter != null);
            this.UpdateStats();
        }

        private void UpdateStats()
        {
            if (_currentCharacter != null)
            {
                int remainMoves = _currentCharacter.GetValue(GameEntityAPI.CurrentMovesCount).Value;
                int remainAttacks = _currentCharacter.GetValue(GameEntityAPI.CurrentAttacksCount).Value;
                _view.text = $"Moves: {remainMoves}\nAttacks: {remainAttacks}";
            }
        }

        private void OnMovesChanged(int _) => this.UpdateStats();

        private void OnAttacksChanged(int _) => this.UpdateStats();
    }
}
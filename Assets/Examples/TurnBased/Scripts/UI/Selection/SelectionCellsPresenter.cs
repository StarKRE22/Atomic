using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class SelectionCellsPresenter : IUIContextInit, IUIContextEnable, IUIContextDisable
    {
        private readonly IGameContext _gameContext;
        
        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private GameBoardView _gameBoardView;
        private Subscription<IGameEntity> _subscription;

        public SelectionCellsPresenter(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUIContext ui)
        {
            _gameBoardView = ui.GetValue(UIContextAPI.GameBoardView);
            _selectedCharacter = ui.GetValue(UIContextAPI.SelectedCharacter);
        }
        
        public void Enable(IUIContext entity)
        {
            _subscription = _selectedCharacter.Subscribe(this.OnCharacterChanged);
        }

        public void Disable(IUIContext entity)
        {
            _subscription.Dispose();
        }

        private void OnCharacterChanged(IGameEntity entity)
        {
            _gameBoardView.ResetHighlights();

            if (entity == null)
                return;

            // var attackPositions = entity.GetAvailableAttackPositions(_gameContext);

            var movePositions = entity.GetAvailableMovePositions(_gameContext);
            _gameBoardView.SetMoveEnabled(movePositions);
            // _gameBoardView.SetAttackEnabled(attackPositions);
        }
    }
}
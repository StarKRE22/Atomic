using System;
using Atomic.Entities;
using Game.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [Serializable]
    public sealed class HUDInstaller
    {
        [SerializeField] private TurnView _turnView;
        [SerializeField] private TMP_Text _turnEventsText;
        [SerializeField] private TMP_Text _selectedCharacterInfoText;
        [SerializeField] private TMP_Text _gameStateText;
        [SerializeField] private TMP_Text _currentTurnText;
        [SerializeField] private Button _endTurnButton;
        
        public void Install(IUIContext ui, IGameContext gameContext)
        {
            ui.AddTurnView( _turnView);
            ui.AddEndTurnButton( _endTurnButton);
            
            ui.AddBehaviour(new TurnEventsPresenter(_turnEventsText, gameContext));
            ui.AddBehaviour(new SelectedCharacterStatsPresenter(_selectedCharacterInfoText));
            ui.AddBehaviour(new GameStatePresenter(_gameStateText, gameContext));
            ui.AddBehaviour(new CurrentTurnPresenter(_currentTurnText, gameContext));
            ui.AddBehaviour(new EndTurnButtonPresenter(_endTurnButton, gameContext));
            
            ui.AddBehaviour(new PlayerTurnPresenter(gameContext));
            ui.AddBehaviour(new EnemyTurnPresenter(gameContext));
        }
    }
}
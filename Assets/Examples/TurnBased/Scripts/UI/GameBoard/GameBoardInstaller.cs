using System;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    [Serializable]
    public sealed class GameBoardInstaller
    {
        [SerializeField] private GameBoardView _gameBoardView;

        public void Install(IUIContext ui, IGameContext gameContext)
        {
            ui.AddGameBoardView( _gameBoardView);
            ui.AddBehaviour(new GameBoardPresenter(gameContext));
        }
    }
}
using System;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class GameBoardInstaller : IGameContextInstaller
    {
        [SerializeField]
        private Vector2Int _gameBoardSize = new(6, 6);

        public void Install(IGameContext context)
        {
            GameEntityBoard entityBoard = new GameEntityBoard(_gameBoardSize.x, _gameBoardSize.y);
            context.AddGameBoard( entityBoard);
            context.AddPathFinder( new GameBoardPathFinder(entityBoard));
        }
    }
}
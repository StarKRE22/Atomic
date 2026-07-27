using Atomic.Entities;
using UnityEngine;

namespace Game.UI
{
    public static class GameBoardViewUseCase
    {
        public static Vector3 GetWorldPosition(this IUIContext context, Vector2Int position)
        {
            GameBoardView boardView = context.GetGameBoardView();
            return boardView.ToWorldPosition(position.x, position.y);
        }
    }
}
using Atomic.Entities;
using UnityEngine;

namespace Game.UI
{
    public static class GameBoardViewUseCase
    {
        public static Vector3 GetWorldPosition(this IUIContext context, Vector2Int position)
        {
            GameBoardView boardView = context.GetValue(UIContextAPI.GameBoardView);
            return boardView.ToWorldPosition(position.x, position.y);
        }
    }
}
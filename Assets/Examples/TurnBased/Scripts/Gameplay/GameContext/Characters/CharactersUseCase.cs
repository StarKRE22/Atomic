using System.Linq;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static class CharactersUseCase
    {
        public static bool AnyAliveCharacters(this IGameContext context) =>
            context.GetCharacters().AnyAlive();

        public static IGameEntity[] GetCharacters(this IGameContext context)
        {
            GameEntityBoard entityBoard = context.GetGameBoard();
            return entityBoard.Entities.Keys
                .Where(e => e.GetEntityType().Value == GameEntityType.Character)
                .ToArray();
        }
    }
}
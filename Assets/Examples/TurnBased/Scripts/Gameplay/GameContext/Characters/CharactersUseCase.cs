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
            GameEntityBoard entityBoard = context.GetValue(GameContextAPI.GameBoard);
            return entityBoard.Entities.Keys
                .Where(e => e.GetValue(GameEntityAPI.EntityType).Value == GameEntityType.Character)
                .ToArray();
        }
    }
}
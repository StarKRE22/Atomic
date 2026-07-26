using System.Collections.Generic;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static class StartTurnUseCase
    {
        public static void ResetTurn(this IEnumerable<IGameEntity> entities)
        {
            foreach (IGameEntity entity in entities) 
                entity.GetValue(GameEntityAPI.ResetTurnAction).Invoke();
        }
    }
}
using Atomic.Elements;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameEntityAPI
    {
        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> Health = new(nameof(Health));
        public static readonly ValueKey<IGameEntity, IValue<int>> MaxHealth = new(nameof(MaxHealth));
    }
}
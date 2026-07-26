using Atomic.Elements;
using Atomic.Entities;

namespace Game.Gameplay
{
    public static partial class GameEntityAPI
    {
        public static readonly ValueKey<IGameEntity, IValue<int>> AttackDamage =
            new(nameof(AttackDamage));

        public static readonly ValueKey<IGameEntity, IValue<int>> AttackDistance =
            new(nameof(AttackDistance));

        public static readonly ValueKey<IGameEntity, IValue<int>> MaxAttacksPerTurn =
            new(nameof(MaxAttacksPerTurn));

        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> CurrentAttacksCount =
            new(nameof(CurrentAttacksCount));
    }
}
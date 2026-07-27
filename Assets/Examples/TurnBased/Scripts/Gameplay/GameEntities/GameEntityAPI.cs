using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    [EntityAPI]
    public static partial class GameEntityAPI
    {
        public static readonly ValueKey<IGameEntity, IValue<GameEntityType>> EntityType = new(nameof(EntityType));
        public static readonly ValueKey<IGameEntity, IAction> ResetTurnAction = new(nameof(ResetTurnAction));
        public static readonly ValueKey<IGameEntity, IAction> RespawnAction = new(nameof(RespawnAction));
        public static readonly ValueKey<IGameEntity, IFunction<IGameContext, UniTask>> MakeTurnAction = new(nameof(MakeTurnAction));
        public static readonly ValueKey<IGameEntity, IValue<int>> PushDamage = new(nameof(PushDamage));

        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> Health = new(nameof(Health));
        public static readonly ValueKey<IGameEntity, IValue<int>> MaxHealth = new(nameof(MaxHealth));

        public static readonly ValueKey<IGameEntity, IValue<int>> MaxMovesPerTurn = new(nameof(MaxMovesPerTurn));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> CurrentMovesCount = new(nameof(CurrentMovesCount));

        public static readonly ValueKey<IGameEntity, IValue<int>> AttackDamage = new(nameof(AttackDamage));
        public static readonly ValueKey<IGameEntity, IValue<int>> AttackDistance = new(nameof(AttackDistance));
        public static readonly ValueKey<IGameEntity, IValue<int>> MaxAttacksPerTurn = new(nameof(MaxAttacksPerTurn));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<int>> CurrentAttacksCount = new(nameof(CurrentAttacksCount));
    }
}

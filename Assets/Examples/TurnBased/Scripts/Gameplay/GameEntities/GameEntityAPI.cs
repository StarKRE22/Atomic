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
    }
}

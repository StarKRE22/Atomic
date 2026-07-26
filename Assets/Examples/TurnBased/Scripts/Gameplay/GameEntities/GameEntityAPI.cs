using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public static partial class GameEntityAPI
    {
        // Type
        public static readonly ValueKey<IGameEntity, IValue<GameEntityType>> EntityType = new(nameof(EntityType));

        // Reset
        public static readonly ValueKey<IGameEntity, IAction> ResetTurnAction = new(nameof(ResetTurnAction));
        public static readonly ValueKey<IGameEntity, IAction> RespawnAction = new(nameof(RespawnAction));
        
        // AI
        public static readonly ValueKey<IGameEntity, IFunction<IGameContext, UniTask>> MakeTurnAction = new(nameof(MakeTurnAction));

        // Push
        public static readonly ValueKey<IGameEntity, IValue<int>> PushDamage = new(nameof(PushDamage));
    }
}
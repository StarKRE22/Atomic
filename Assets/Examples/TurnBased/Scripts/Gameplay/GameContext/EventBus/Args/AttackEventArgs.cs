using UnityEngine;

namespace Game.Gameplay
{
    public readonly struct AttackEventArgs
    {
        public readonly IGameEntity instigator;
        public readonly IGameEntity victim;
        public readonly Vector2Int sourcePosition;
        public readonly Vector2Int targetPosition;

        public AttackEventArgs(IGameEntity instigator, IGameEntity victim, Vector2Int sourcePosition, Vector2Int targetPosition)
        {
            this.instigator = instigator;
            this.victim = victim;
            this.sourcePosition = sourcePosition;
            this.targetPosition = targetPosition;
        }
    }
}
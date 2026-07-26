using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class MoveEntityInstaller : IGameEntityInstaller
    {
        [SerializeField]
        private Const<int> _movesPerTurn = 1;

        public void Install(IGameEntity entity)
        {
            entity.AddValue(GameEntityAPI.CurrentMovesCount, new ThreadSafeReactiveVariable<int>());
            entity.AddValue(GameEntityAPI.MaxMovesPerTurn, _movesPerTurn);
        }
    }
}
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
            entity.AddCurrentMovesCount( new ThreadSafeReactiveVariable<int>());
            entity.AddMaxMovesPerTurn( _movesPerTurn);
        }
    }
}
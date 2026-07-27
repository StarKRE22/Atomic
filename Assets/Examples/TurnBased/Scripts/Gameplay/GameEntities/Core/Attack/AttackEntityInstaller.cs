using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class AttackEntityInstaller : IGameEntityInstaller
    {
        [SerializeField]
        private Const<int> _attacksPerTurn = 1;
        
        [SerializeField]
        private Const<int> _attackDamage = 1;
        
        [SerializeField]
        private Const<int> _attackDistance = 1;

        public void Install(IGameEntity entity)
        {
            entity.AddAttackDamage( _attackDamage);
            entity.AddAttackDistance( _attackDistance);
            entity.AddMaxAttacksPerTurn( _attacksPerTurn);
            entity.AddCurrentAttacksCount( new ThreadSafeReactiveVariable<int>());
        }
    }
}
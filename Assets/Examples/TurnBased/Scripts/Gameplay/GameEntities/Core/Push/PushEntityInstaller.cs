using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class PushEntityInstaller : IGameEntityInstaller
    {
        [SerializeField]
        private Const<int> _pushDamage = 1;

        public void Install(IGameEntity entity)
        {
            entity.AddValue(GameEntityAPI.PushDamage, _pushDamage);
        }
    }
}
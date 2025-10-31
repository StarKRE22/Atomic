using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CoinInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private ReactiveVariable<int> _money = 1;

        public override void Install(IEntity entity)
        {
            entity.AddCoinTag();
            entity.AddTransform(this.transform);
            entity.AddMoney(_money);
        }
    }
}
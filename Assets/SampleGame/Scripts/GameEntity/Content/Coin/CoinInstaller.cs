using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class CoinInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<int> _money = 1;

        protected override void Install(IGameEntity entity)
        {
            entity.AddCoinTag();
            entity.AddGameObject(this.gameObject);
            entity.AddTransform(this.transform);
            entity.AddMoney(_money);
        }
    }
}
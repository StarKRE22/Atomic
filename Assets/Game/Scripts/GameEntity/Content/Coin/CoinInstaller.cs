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
            entity.AddPosition(new ProxyVariable<Vector3>(
                getter: () => this.transform.position,
                setter: value => this.transform.position = value)
            );
            entity.AddRotation(new ProxyVariable<Quaternion>(
                getter: () => this.transform.rotation,
                setter: value => this.transform.rotation = value)
            );
            entity.AddMoney(_money);
        }
    }
}
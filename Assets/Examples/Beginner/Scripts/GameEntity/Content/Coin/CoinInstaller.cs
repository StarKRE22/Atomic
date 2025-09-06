using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CoinInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<int> _money = 1;

        public override void Install(IGameEntity entity)
        {
            entity.AddCoinTag();
            entity.AddPosition(new TransformPositionVariable(this.transform));
            entity.AddRotation(new TransformRotationVariable(this.transform));
            entity.AddMoney(_money);
        }
    }
}
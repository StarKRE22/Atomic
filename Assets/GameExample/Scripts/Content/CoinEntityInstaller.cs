using Atomic.Entities;
using Atomic.Extensions;
using GameExample.Engine;
using UnityEngine;

namespace GameExample.Content
{
    public sealed class CoinEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField]
        private int money;
        
        public override void Install(IEntity entity)
        {
            entity.AddCoinTag();
            entity.AddGameObject(this.gameObject);
            entity.AddTransform(this.transform);
            
            entity.AddPosition(new float3Reactive(this.transform.position));
            entity.AddRotation(new quaternionReactive(this.transform.rotation));
            entity.AddBehaviour<TransformBehaviour>();

            entity.AddMoney(this.money);
        }
    }
}
using Atomic.Contexts;
using Atomic.Elements;
using GameExample.Engine;

namespace Walkthrough
{
    public sealed class CoinCollectInstaller : SceneContextInstallerBase
    {
        public override void Install(IContext context)
        {
            context.AddMoney(new ReactiveVariable<int>());
            context.AddSystem<CoinCollectSystem>();
        }
    }
}
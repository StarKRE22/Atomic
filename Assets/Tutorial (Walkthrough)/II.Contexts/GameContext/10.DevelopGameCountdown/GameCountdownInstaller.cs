using Atomic.Contexts;
using Atomic.Elements;
using GameExample.Engine;
using UnityEngine;

namespace Walkthrough
{
    public sealed class GameCountdownInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private float duration = 60;
        
        public override void Install(IContext context)
        {
            var countdown = new Countdown(this.duration);
            countdown.OnCurrentTimeChanged += time => Debug.Log($"Remaining Time {time}");
            
            context.AddGameCountdown(countdown);
            context.WhenEnable(() => countdown.Start());
            context.WhenUpdate(deltaTime => countdown.Tick(deltaTime));
            context.WhenDisable(() => countdown.Stop());

            context.AddSystem<GameOverController>();
        }
    }
}
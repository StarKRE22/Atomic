using System;
using Atomic.Contexts;
using Atomic.Elements;
using UnityEngine;

namespace GameExample.Content
{
    [Serializable]
    public sealed class GameCountdownInstaller : IContextInstaller
    {
        [SerializeField]
        private float duration = 60;
        
        public void Install(IContext context)
        {
            var countdown = new Countdown(this.duration);
            context.AddGameCountdown(countdown);
            context.WhenEnable(() => countdown.Start());
            context.WhenUpdate(deltaTime => countdown.Tick(deltaTime));
            context.WhenDisable(() => countdown.Stop());     
        }
    }
}
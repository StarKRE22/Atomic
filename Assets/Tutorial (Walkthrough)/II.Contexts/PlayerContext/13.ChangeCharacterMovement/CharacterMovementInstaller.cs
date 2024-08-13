using Atomic.Contexts;
using GameExample.Engine;
using UnityEngine;

namespace Walkthrough
{
    public sealed class CharacterMovementInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private InputMap inputMap;
        
        public override void Install(IContext context)
        {
            context.AddInputMap(this.inputMap);
            context.AddSystem<CharacterMovementSystem>();
        }
    }
}
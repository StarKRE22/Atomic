using Atomic.Contexts;
using Atomic.Entities;
using UnityEngine;

namespace Walkthrough
{
    public sealed class PlayerCharacterInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private SceneEntity character;

        public override void Install(IContext context)
        {
            Debug.Log("Install Character");
            context.AddCharacter(this.character);
        }
    }
}
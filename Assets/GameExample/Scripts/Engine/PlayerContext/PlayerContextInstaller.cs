using System.Linq;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using GameExample.Engine;
using UnityEngine;

namespace GameExample.Content
{
    public sealed class PlayerContextInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private SceneEntity character;
        
        [SerializeField]
        private InputMap inputMap;

        [SerializeField]
        private CameraData cameraData;

        [SerializeField]
        private TeamType teamType;

        public override void Install(IContext context)
        {
            context.GetPlayerMap().Add(this.teamType, context);
            context.AddTeamType(this.teamType);

            context.AddCharacter(new Const<IEntity>(this.character));
            context.AddInputMap(this.inputMap);
            context.AddCameraData(this.cameraData);
            context.AddMoney(new ReactiveVariable<int>());
            
            context.AddSystem<CharacterMovementSystem>();
            context.AddSystem<CoinCollectSystem>();
            context.AddSystem<CameraFollowSystem>();
        }
    }
}
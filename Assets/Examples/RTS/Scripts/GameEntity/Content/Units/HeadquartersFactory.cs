using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "HeadquartersFactory",
        menuName = "RTSGame/GameEntities/New HeadquartersFactory"
    )]
    public sealed class HeadquartersFactory : GameEntityFactory
    {
        [SerializeField]
        private TransformEntityInstaller _transformInstaller;
        
        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        public override IGameEntity Create()
        {
            var entity = base.Create();
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
        
            _transformInstaller.Install(entity);
            _lifeInstaller.Install(entity);
            return entity;
        }
    }
}


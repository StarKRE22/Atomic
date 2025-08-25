using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersBaker : GameEntityBaker
    {
        [SerializeField]
        private TransformEntityInstaller _transformInstaller;
        
        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        protected override void Install(IGameEntity entity)
        {
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
        
            _transformInstaller.Install(entity);
            _lifeInstaller.Install(entity);
        } 
    }
}
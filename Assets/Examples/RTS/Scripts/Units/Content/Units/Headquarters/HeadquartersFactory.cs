using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "HeadquartersFactory",
        menuName = "RTSGame/GameEntities/New HeadquartersFactory"
    )]
    public sealed class HeadquartersFactory : UnitFactory
    {
        [SerializeField]
        private TransformEntityInstaller _transformInstaller;
        
        [SerializeField]
        private LifeEntityInstaller _lifeInstaller;

        protected override void Install(IUnit entity)
        {
            IGameContext gameContext = GameContext.Instance;
            
            entity.AddUnitTag();
            entity.AddTeam(new ReactiveVariable<TeamType>());
            entity.Install(_transformInstaller);
            entity.Install(_lifeInstaller);
            entity.AddBehaviour(new StaticSpatialHashBehaviour(gameContext));
        }
    }
}


using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CharacterViewInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private MoneyView _moneyView;
        
        [SerializeField]
        private Transform _canvas;

        protected override void Install(IGameEntity entity)
        {
            //Team:
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
            
            //Money:
            entity.AddMoneyView(_moneyView);
            entity.AddBehaviour<MoneyPresenter>();
            
            //Billboard:
            entity.AddBehaviour(new CameraBillboardBehaviour(_canvas));
        }
    }
}
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public class CharacterViewInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private MeshRenderer _renderer;
        
        protected override void Install(IGameEntity entity)
        {
            entity.AddRenderer(_renderer);
            entity.AddBehaviour<TeamColorBehaviour>();
            entity.AddBehaviour<CharacterNameBehaviour>();
        }
    }
}
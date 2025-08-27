using Atomic.Entities;
using ShooterGame;
using ShooterGame.Gameplay;
using UnityEngine;

namespace RTSGame
{
    public sealed class CameraFollowController : IEntitySpawn<IPlayerContext>, IEntityLateUpdate
    {
        private readonly Vector3 _offset;
        private IGameEntity _character;
        private Transform _camera;
        
        public CameraFollowController(Vector3 offset)
        {
            _offset = offset;
        }

        public void OnSpawn(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _camera = context.GetCamera().transform;
        }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            _camera.position = _character.GetPosition().Value + _offset;
        }
    }
}
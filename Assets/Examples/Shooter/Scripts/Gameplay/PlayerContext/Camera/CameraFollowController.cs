using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CameraFollowController : IEntityInit<IPlayerContext>, IEntityLateTick
    {
        private readonly Vector3 _offset;
        private IWorldEntity _character;
        private Transform _camera;
        
        public CameraFollowController(Vector3 offset)
        {
            _offset = offset;
        }

        public void Init(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _camera = context.GetCamera().transform;
        }

        public void LateTick(IEntity entity, float deltaTime)
        {
            _camera.position = _character.GetPosition().Value + _offset;
        }
    }
}
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CameraFollowController : IPlayerContextInit, IPlayerContextLateTick
    {
        private readonly Vector3 _offset;
        private IGameEntity _character;
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

        public void LateTick(IPlayerContext entity, float deltaTime)
        {
            _camera.position = _character.GetPosition().Value + _offset;
        }
    }
}
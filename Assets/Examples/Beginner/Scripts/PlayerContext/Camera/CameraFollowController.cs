using System.Runtime.CompilerServices;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraFollowController :
        IEntitySpawn<IPlayerContext>,
        IEntityActivate,
        IEntityLateUpdate
    {
        private readonly Vector3 _cameraOffset;
        private Transform _camera;
        private IGameEntity _character;

        public CameraFollowController(Vector3 cameraOffset)
        {
            _cameraOffset = cameraOffset;
        }

        public void OnSpawn(IPlayerContext entity)
        {
            _camera = entity.GetCamera().transform;
            _character = entity.GetCharacter();
        }

        public void OnActivate(IEntity entity) => this.UpdatePosition();

        public void OnLateUpdate(IEntity entity, float deltaTime) => this.UpdatePosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdatePosition()
        {
            _camera.position = _character.GetPosition().Value + _cameraOffset;
        }
    }
}
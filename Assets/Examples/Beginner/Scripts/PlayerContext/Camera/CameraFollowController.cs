using System.Runtime.CompilerServices;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraFollowController :
        IEntitySpawn<IPlayerContext>,
        IEntityActive,
        IEntityLateUpdate
    {
        private readonly Transform _cameraRoot;
        private readonly Vector3 _cameraOffset;

        private IGameEntity _character;

        public CameraFollowController(Transform cameraRoot, Vector3 cameraOffset)
        {
            _cameraRoot = cameraRoot;
            _cameraOffset = cameraOffset;
        }

        public void OnSpawn(IPlayerContext entity) => _character = entity.GetCharacter();

        public void OnActive(IEntity entity) => this.UpdatePosition();

        public void OnLateUpdate(IEntity entity, float deltaTime) => this.UpdatePosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdatePosition()
        {
            _cameraRoot.position = _character.GetPosition().Value + _cameraOffset;
        }
    }
}
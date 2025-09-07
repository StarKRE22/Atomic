using System;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraBillboardBehaviour : IEntityInit<IGameEntity>, IEntityLateTick
    {
        private readonly Transform _target;
        private Transform _camera;

        public CameraBillboardBehaviour(Transform target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public void Init(IGameEntity entity)
        {
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(GameContext.Instance, entity);
            _camera = playerContext.GetCamera().transform;
        }

        public void LateTick(IEntity entity, float deltaTime)
        {
            Vector3 dir = _target.position - _camera.position;
            _target.rotation = Quaternion.LookRotation(dir, _camera.up);
        }
    }
}
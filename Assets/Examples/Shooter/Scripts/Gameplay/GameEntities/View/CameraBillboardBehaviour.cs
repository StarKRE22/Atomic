using System;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CameraBillboardBehaviour : IGameEntityInit, IGameEntityLateTick
    {
        private readonly IGameContext _gameContext;
        private readonly Transform _target;
        private Transform _camera;

        public CameraBillboardBehaviour(IGameContext gameContext, Transform target)
        {
            _gameContext = gameContext;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public void Init(IGameEntity entity)
        {
            IPlayerContext playerContext = PlayersUseCase.GetPlayer(_gameContext, entity);
            _camera = playerContext.GetCamera().transform;
        }

        public void LateTick(IGameEntity entity, float deltaTime)
        {
            Vector3 dir = _target.position - _camera.position;
            _target.rotation = Quaternion.LookRotation(dir, _camera.up);
        }
    }
}
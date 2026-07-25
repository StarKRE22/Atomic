using Atomic.Elements;
using Modules.SpatialStructures;
using UnityEngine;

namespace RTSGame
{
    public sealed class SpatialGridBehaviour : IGameEntityInit, IGameEntityEnable, IGameEntityDisable
    {
        private const float POSITION_THRESHOLD_SQR = 0.04f; //0.2

        private IReactiveValue<Vector3> _position;
        private SpatialGrid2D<IGameEntity> _spatialGrid;
        private IGameEntity _entity;

        private readonly IGameContext _gameContext;

        private Vector2 _previousPosition;

        public SpatialGridBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _position = entity.GetPosition();
            _spatialGrid = _gameContext.GetEntitySpace();
        }

        public void Enable(IGameEntity entity)
        {
            Vector3 position3D = _position.Value;
            
            _previousPosition = new Vector2(position3D.x, position3D.z);
            _spatialGrid.Insert(entity, _previousPosition);
            _position.OnEvent += this.OnPositionChanged;
        }

        public void Disable(IGameEntity entity)
        {
            _position.OnEvent -= this.OnPositionChanged;
            _spatialGrid.Remove(entity);
        }

        private void OnPositionChanged(Vector3 position3D)
        {
            Vector2 position2D = new Vector2(position3D.x, position3D.z);
            if (Vector2.SqrMagnitude(position2D - _previousPosition) >= POSITION_THRESHOLD_SQR)
            {
                _spatialGrid.Move(_entity, position2D);
                _previousPosition = position2D;
            }
        }
    }
}
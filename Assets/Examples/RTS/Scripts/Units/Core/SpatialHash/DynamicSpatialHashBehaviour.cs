using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class DynamicSpatialHashBehaviour :
        IEntityInit<IUnit>,
        IEntityEnable,
        IEntityDisable,
        IEntityFixedTick
    {
        private readonly IGameContext _gameContext;
        private IUnit _self;
        private SpatialHash<IUnit> _spatialHash;
        private IValue<Vector3> _position;

        private Vector3Int _key; // текущая клетка
        private Vector3Int _newKey; // буфер
        private Vector3 _oldPos;

        public DynamicSpatialHashBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUnit entity)
        {
            _self = entity;
            _spatialHash = _gameContext.GetSpatialHash();
            _position = entity.GetPosition();
        }

        public void Enable(IEntity entity)
        {
            // Вставляем в spatial hash и получаем клетку
            _spatialHash.Insert(_self, out _key);
            _oldPos = _position.Value;
        }

        public void Disable(IEntity entity)
        {
            _spatialHash.Remove(_self);
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            Vector3 pos = _position.Value;

            // Позиция не изменилась — выходим
            if (pos == _oldPos)
                return;

            // Вычисляем новую клетку только один раз (быстро)
            _newKey = _spatialHash.HashPublic(pos);

            // Если клетка не поменялась — ничего не делаем
            if (_newKey == _key)
            {
                _oldPos = pos;
                return;
            }

            // Удаляем из старой клетки
            _spatialHash.Move(_self, _key, _newKey);

            // Обновляем текущий ключ
            _key = _newKey;
            _oldPos = pos;
        }
    }
}